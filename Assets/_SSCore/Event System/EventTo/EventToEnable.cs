using UnityEngine;
using System.Collections;
using SS;

public class EventToEnable : EventListener
{
	public Component targetComp;
	
	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		if (targetComp != null)
		{
			Behaviour behavior = targetComp as Behaviour;
			Renderer ren = targetComp as Renderer;
			Collider coll = targetComp as Collider;

			if (behavior)
			{
				behavior.enabled = paramBool;
			}
			if (ren)
			{
				ren.enabled = paramBool;
			}
			if (coll)
			{
				coll.enabled = paramBool;
			}
		}
	}
}