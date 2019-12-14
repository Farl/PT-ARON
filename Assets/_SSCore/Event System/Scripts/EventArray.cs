using UnityEngine;
using System.Collections;

namespace SS
{
	
	[System.Serializable]
	public class EventMsg
	{
		public string m_eventID;
		public GameObject[] m_targetObj;
		public bool m_paramBool;
		public string m_paramString;
		public Vector2 m_delayTime;
		public bool m_useTimeScale;
	}
	
	[System.Serializable]
	public class EventArray
	{
		[Auto]
		public EventMsg[] eventArray;

		public void Broadcast(Object obj)
		{
			if (eventArray == null)
				return;

			foreach (EventMsg info in eventArray)
			{
				bool bSend = false;

				if (info.m_targetObj != null)
				{
					foreach (GameObject target in info.m_targetObj)
					{
						if (target != null)
						{
							EventManager.SendObjectEvent(target, info.m_eventID, obj, info.m_paramBool, info.m_paramString, info.m_delayTime, info.m_useTimeScale);
							bSend = true;
						}
					}
				}

				if (!bSend)
				{
					EventManager.Broadcast(info.m_eventID, obj, info.m_paramBool, info.m_paramString, info.m_delayTime, info.m_useTimeScale);
				}
			}
		}
	}

}