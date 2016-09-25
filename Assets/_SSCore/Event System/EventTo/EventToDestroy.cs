using UnityEngine;
using System.Collections;
using SS;

public class EventToDestroy : EventListener
{
	public Object targetObj;
	public Object[] targetObjs;
	
	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		if (targetObj != null)
		{
			Destroy(targetObj);
		}
		if (targetObjs != null)
		{
			foreach(Object obj in targetObjs)
			{
				if (obj != null)
				{
					Destroy(obj);
				}
			}
		}
	}
}
