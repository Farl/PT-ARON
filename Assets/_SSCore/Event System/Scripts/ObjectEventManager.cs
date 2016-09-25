using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SS
{
	public class ObjectEventManager : MonoBehaviour
	{
		// Event Table. event ID and event delegate mapping
		public Hashtable eventTable = new Hashtable();

		EventInfo GetEventInfo(string eventID)
		{
			if (!eventTable.ContainsKey(eventID))
			{
				eventTable[eventID] = new EventInfo(eventID);
			}
			return (EventInfo)eventTable[eventID];
		}
		
		public void AddEventListener(string eventID, EventDelegate ed, GameObject go)
		{
			EventInfo ei = GetEventInfo (eventID);
			if (ei != null)
				ei.AddDelegate(ed);
		}
		
		public void RemoveEventListener(string eventID, EventDelegate ed, GameObject go)
		{
			EventInfo ei = GetEventInfo (eventID);
			if (ei != null)
				ei.RemoveDelegate(ed);
		}

		// Send Object Event
		public void SendObjectEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
		{
			EventInfo ei = GetEventInfo (eventID);
			if (ei != null && ei.m_delegate != null)
			{
				ei.m_delegate(eventID, origSrc, paramBool, paramString, ref paramRef, paramExtra);
			}
			return;
		}
	}
}