using UnityEngine;
using System.Collections;
using SS;

public class EventToAnim : EventListener
{
	public Animation targetAnim;
	public string clipName;
	public AnimationClip clip;

	
	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		if (targetAnim == null)
			return;
		if (clipName != null)
		{
			AnimationState animState = targetAnim[clipName];
			if (animState)
			{
				targetAnim.CrossFade(clipName);
				return;
			}
		}
		if (clip != null)
		{
			targetAnim.AddClip(clip, clip.name);
			targetAnim.CrossFade(clip.name);
		}
	}
}
