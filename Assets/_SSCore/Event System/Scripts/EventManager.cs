using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SS
{
	// Event Delegate
	public delegate void EventDelegate(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra);
	
	// Event Info
	public class EventInfo
	{
		public string m_eventID;
		public EventDelegate m_delegate;
		
		public EventInfo(string eventID)
		{
			m_eventID = eventID;
		}
		public void AddDelegate(EventDelegate ed)
		{
			m_delegate += ed;
		}
		public void RemoveDelegate(EventDelegate ed)
		{
			m_delegate -= ed;
		}
	}

	//
	public class EventMessage
	{
		public string eventID;
		public UnityEngine.Object origSrc;
		public bool paramBool;
		public string paramString;
		public object[] paramExtra;
		public Vector2 delayTime;
		public GameObject targetObj;

		// Global event
		public EventMessage(
			string _eventID,
			UnityEngine.Object _origSrc,
			bool _paramBool,
			string _paramString,
			Vector2 _delayTime,
			object[] _paramExtra
			)
		{
			eventID = _eventID;
			origSrc = _origSrc;
			paramBool = _paramBool;
			paramString = _paramString;
			delayTime = _delayTime;
			paramExtra = _paramExtra;
		}

		// Object event
		public EventMessage(
			GameObject _targetObj,
			string _eventID,
			UnityEngine.Object _origSrc,
			bool _paramBool,
			string _paramString,
			Vector2 _delayTime,
			object[] _paramExtra
			)
		{
			targetObj = _targetObj;
			eventID = _eventID;
			origSrc = _origSrc;
			paramBool = _paramBool;
			paramString = _paramString;
			delayTime = _delayTime;
			paramExtra = _paramExtra;
		}
	}

	// Event Manager
	public static class EventManager
	{
		// Event Table. event ID and event delegate mapping
		public static Hashtable eventTable = new Hashtable();

		//
		static EventManager()
		{
			AddEventListener("Debug", DebugFunc);
		}

		// Debug
		static void DebugFunc(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
		{
			Debug.Log(eventID);
			return;
		}
		
		static EventInfo GetEventInfo(string eventID)
		{
			if (!eventTable.ContainsKey(eventID))
			{
				eventTable[eventID] = new EventInfo(eventID);
			}
			return (EventInfo)eventTable[eventID];
		}

		// Add Event Listener
		public static void AddEventListener(string eventID, EventDelegate ed, GameObject go = null)
		{
			if (go != null)
			{
				ObjectEventManager objManager = go.GetComponent<ObjectEventManager>();
				if (objManager == null)
					objManager = go.AddComponent<ObjectEventManager>();
				if (objManager != null)
					objManager.AddEventListener(eventID, ed, go);
			}
			else
			{
				EventInfo ei = GetEventInfo (eventID);
				if (ei != null)
					ei.AddDelegate(ed);
			}
		}

		// Remove Event Listener
		public static void RemoveEventListener(string eventID, EventDelegate ed, GameObject go = null)
		{
			if (go != null)
			{
				ObjectEventManager objManager = go.GetComponent<ObjectEventManager>();
				if (objManager == null)
					objManager = go.AddComponent<ObjectEventManager>();
				if (objManager != null)
					objManager.RemoveEventListener(eventID, ed, go);
			}
			else
			{
				EventInfo ei = GetEventInfo (eventID);
				if (ei != null)
					ei.RemoveDelegate(ed);
			}
		}
		#region Broadcast Global Event
		// Send Global Event
		public static void Broadcast(
			string eventID,
			UnityEngine.Object origSrc,
			bool paramBool,
			string paramString,
			Vector2 delayTime,
			bool useTimeScale = false,
			params object[] paramExtra)
		{
			if (delayTime.x > 0 && delayTime.y > 0)
			{
				EventMessage em = new EventMessage(eventID, origSrc, paramBool, paramString, delayTime, paramExtra);
				EventTimer.AddTimer(em, delayTime, useTimeScale);
			}
			else
			{
				object refObj = null;
				Broadcast(eventID, origSrc, paramBool, paramString, ref refObj, paramExtra);
			}

			return;
		}
		public static void Broadcast(
			string eventID,
			UnityEngine.Object origSrc,
			bool paramBool,
			string paramString,
			ref object paramRef,
			params object[] paramExtra)
		{
			EventInfo ei = GetEventInfo (eventID);
			if (ei != null && ei.m_delegate != null)
			{
				ei.m_delegate(eventID, origSrc, paramBool, paramString, ref paramRef, paramExtra);
			}
			return;
		}
		public static void Broadcast(
			string eventID,
			UnityEngine.Object origSrc = null,
			bool paramBool = true,
			string paramString = null)
		{
			object obj = null;
			Broadcast(eventID, origSrc, paramBool, paramString, ref obj, null);
			return;
		}
		#endregion

		#region Object Event

		// Send Object Event

		public static void SendObjectEvent(
			GameObject targetObj,
			string eventID,
			UnityEngine.Object origSrc,
			bool paramBool,
			string paramString,
			Vector2 timeDelay,
			bool useTimeScale = false,
			params object[] paramExtra)
		{
			if (timeDelay.x > 0 && timeDelay.y > 0)
			{
				EventMessage em = new EventMessage(targetObj, eventID, origSrc, paramBool, paramString, timeDelay, paramExtra);
				EventTimer.AddTimer(em, timeDelay, useTimeScale);
			}
			else
			{
				object refObj = null;
				SendObjectEvent(targetObj, eventID, origSrc, paramBool, paramString, ref refObj, paramExtra);
			}
			return;
		}

		public static void SendObjectEvent(
			GameObject targetObj,
			string eventID,
			UnityEngine.Object origSrc,
			bool paramBool,
			string paramString ,
			ref object paramRef,
			params object[] paramExtra)
		{
			ObjectEventManager[] objManagers = targetObj.GetComponentsInChildren<ObjectEventManager>();
			foreach (ObjectEventManager objManager in objManagers)
			{
				objManager.SendObjectEvent(eventID, origSrc, paramBool, paramString, ref paramRef, paramExtra);
			}
			return;
		}
		#endregion
	}
}