using UnityEngine;
using System.Collections;
using SS;

public class EventToActivate : EventListener
{
	public GameObject targetObj;

	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		OnEvent ();
		if (targetObj != null)
			targetObj.SetActive(paramBool);
		else
			gameObject.SetActive(paramBool);
	}
}
