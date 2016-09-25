using UnityEngine;
using System.Collections;
using SS;

public class EventToParticleSystem : EventListener
{
	
	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		OnEvent();
		if (GetComponent<ParticleSystem>() != null)
		{
			GetComponent<ParticleSystem>().Play(true);
		}
	}
}
