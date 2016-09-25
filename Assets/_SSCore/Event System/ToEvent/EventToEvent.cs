using UnityEngine;
using System.Collections;
using SS;

public class EventToEvent : EventListener
{
	public EventArray eventArray;
	
	[ContextMenu("Do Event")]
	public void DoEvent()
	{
		eventArray.Broadcast(this);
	}

	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		OnEvent();
		DoEvent();
	}
}
