using UnityEngine;
using System.Collections;
using SS;

public class EventToTextMesh : EventListener
{
	TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
	}
	
	protected override void OnEvent(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		if (textMesh != null && paramString != null)
		{
			textMesh.text = paramString;
		}
	}
}
