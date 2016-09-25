using UnityEngine;
using System.Collections;
using SS;

public class UIEventToEvent : MonoBehaviour
{
	public enum UIEventType
	{
		CLICK,
		DOWN,
	}

	public UIEventType type = UIEventType.CLICK;
	public EventArray eventArray;

	void Awake()
	{
		switch(type)
		{
		case UIEventType.CLICK:
			EventManager.AddEventListener("UICamera-Press", OnUIPress, gameObject);
			break;
		case UIEventType.DOWN:
			EventManager.AddEventListener("UICamera-Down", OnUIPress, gameObject);
			break;
		}
	}

	void OnDestroy()
	{
		switch(type)
		{
		case UIEventType.CLICK:
			EventManager.RemoveEventListener("UICamera-Press", OnUIPress, gameObject);
			break;
		case UIEventType.DOWN:
			EventManager.RemoveEventListener("UICamera-Down", OnUIPress, gameObject);
			break;
		}
	}
	
	protected virtual void OnUIPress(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
	{
		switch(type)
		{
		case UIEventType.CLICK:
			if (!paramBool)
				eventArray.Broadcast(this);
			break;
		case UIEventType.DOWN:
			eventArray.Broadcast(this);
			break;
		}
	}
}
