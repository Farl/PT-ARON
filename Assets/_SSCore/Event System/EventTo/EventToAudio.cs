using UnityEngine;
using System.Collections;
using SS;

public class EventToAudio : EventListener
{
	public AudioClip audioClip;

	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		base.OnEvent();
		
		if (audioClip == null)
			return;

		if (paramExtra != null && paramExtra.Length > 0)
		{
			if (paramExtra[0].GetType() == typeof(Vector3))
			{
				AudioSource.PlayClipAtPoint(audioClip, (Vector3)paramExtra[0]);
				return;
			}
		}

		AudioSource.PlayClipAtPoint(audioClip, transform.position);
	}
}
