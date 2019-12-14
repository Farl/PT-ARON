using UnityEngine;
using System.Collections;
using SS;

public class EventToAnimator : EventListener
{
	public Animator targetAnimator;
	public string stateName;
	
	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		if (targetAnimator == null)
		{
			return;
		}

		targetAnimator.Play(stateName);
	}
}
