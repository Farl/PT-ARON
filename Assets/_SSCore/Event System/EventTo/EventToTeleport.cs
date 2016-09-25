using UnityEngine;
using System.Collections;
using SS;

public class EventToTeleport : EventListener
{
	public Transform teleportTo;

	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		if (teleportTo != null)
		{
			transform.position = teleportTo.position;
		}
	}
}
