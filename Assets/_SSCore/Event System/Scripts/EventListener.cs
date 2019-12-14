using UnityEngine;
using System.Collections;
using System;

namespace SS
{
	public class EventListener : MonoBehaviour
	{

		bool m_intialized;
		public string m_eventID;
		public bool m_recvGlobal = true;
		public bool m_recvLocal = false;

		public void Init()
		{
			if (m_intialized)
				return;
			m_intialized = true;
			if (m_recvGlobal)
				EventManager.AddEventListener(m_eventID, OnEvent);
			if (m_recvLocal)
				EventManager.AddEventListener(m_eventID, OnEvent, gameObject);
		}

		public void Delete()
		{
			if (!m_intialized)
				return;
			if (m_recvGlobal)
				EventManager.RemoveEventListener(m_eventID, OnEvent);
			if (m_recvLocal)
				EventManager.RemoveEventListener(m_eventID, OnEvent, gameObject);
		}

		void Awake () {
			Init ();
		}

		void OnDestroy() {
			Delete ();
		}
		
		[ContextMenu("TestEvent(true)")]
		public void TestEventTrue()
		{
			object _ret = null;
			OnEvent(m_eventID, this, true, null, ref _ret);
		}

		[ContextMenu("TestEvent(false)")]
		public void TestEventFalse()
		{
			object _ret = null;
			OnEvent(m_eventID, this, false, null, ref _ret);
		}

		// Full one
		protected virtual void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
		{
			OnEvent ();
		}

		// Simple one
		protected virtual void OnEvent()
		{
		}

		// Call event directly
		public void Go()
		{
			object obj = null;
			OnEvent(m_eventID, this, true, "", ref obj, null);
		}
	}
}