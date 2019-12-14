using UnityEngine;
using System.Collections;
using SS;

public class EventToFog : EventListener
{
	public float fogDensity;
	
	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		enabled = true;
	}

	void Start()
	{
		enabled = false;
	}

	void Update()
	{
		RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, fogDensity, Time.deltaTime);
		if (Mathf.Abs (RenderSettings.fogDensity - fogDensity) < 0.1)
		{
			RenderSettings.fogDensity = fogDensity;
			enabled = false;
		}
	}
}
