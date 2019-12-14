using UnityEngine;
using System.Collections;
using SS;

public class EventToColliderLocker : EventListener
{
	public GameObject targetObj;
	public string flag = "default";
	public bool unlock;
	public bool bRecursive = true;

	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		if (targetObj == null)
			targetObj = gameObject;

		if (unlock)
		{
			if (!paramBool)
			{
				ColliderLocker.Lock(targetObj, flag, bRecursive);
			}
			else
			{
				ColliderLocker.Unlock(targetObj, flag, bRecursive);
			}
		}
		else
		{
			if (paramBool)
			{
				ColliderLocker.Lock(targetObj, flag, bRecursive);
			}
			else
			{
				ColliderLocker.Unlock(targetObj, flag, bRecursive);
			}
		}
	}
}
