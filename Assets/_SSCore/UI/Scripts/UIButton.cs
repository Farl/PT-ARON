using UnityEngine;
using System.Collections;

namespace SS
{
	public class UIButton : MonoBehaviour {
		
		float activeScale = 1.2f;
		Vector3 origScale;

		float currScale;
		float targetScale;
		
		enum ButtonState {
			NORMAL,
			HOVER,
			ACTIVE,
			DISABLED,
			FOCUS
		}

		ButtonState currState = ButtonState.NORMAL;
		
		void Awake()
		{
			origScale = transform.localScale;
			currScale = 1;
			
			EventManager.AddEventListener("UICamera-Press", OnUIPress, gameObject);
			EventManager.AddEventListener("UICamera-Leave", OnUILeave, gameObject);
			
			SetState(ButtonState.NORMAL);
		}
		
		void OnDestroy()
		{
			EventManager.RemoveEventListener("UICamera-Press", OnUIPress, gameObject);
			EventManager.RemoveEventListener("UICamera-Leave", OnUILeave, gameObject);
		}
		
		protected virtual void OnUILeave(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
		{
			SetState(ButtonState.NORMAL);
		}
		
		protected virtual void OnUIPress(string eventID, UnityEngine.Object origSrc, bool paramBool, string paramString, ref object paramRef, params object[] paramExtra)
		{
			if (paramBool)
			{
				SetState(ButtonState.ACTIVE);
			}
			else
			{
				SetState(ButtonState.NORMAL);
			}
		}
		
		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			UpdateState();
			UpdateScale();
		}

		void UpdateScale()
		{
			float newScale = Mathf.Lerp(currScale, targetScale, 0.25f);

			transform.localScale = origScale * newScale;

			currScale = newScale;
		}

		void UpdateState()
		{
			switch (currState)
			{
			case ButtonState.NORMAL:
				break;

			case ButtonState.HOVER:
				break;

			case ButtonState.ACTIVE:
				break;

			case ButtonState.DISABLED:
				break;

			case ButtonState.FOCUS:
				break;
			}
		}
		
		void SetState(ButtonState nextState)
		{
			if (CanEnterState(nextState))
			{
				LeaveState(nextState);
				EnterState(nextState);
				currState = nextState;
			}
		}
		
		bool CanEnterState(ButtonState nextState)
		{
			return true;
		}
		
		void EnterState(ButtonState nextState)
		{
			//Debug.Log (nextState);
			switch (nextState)
			{
			case ButtonState.NORMAL:
				targetScale = 1;
				break;
				
			case ButtonState.HOVER:
				targetScale = 1.1f;
				break;
				
			case ButtonState.ACTIVE:
				targetScale = activeScale;
					break;
				
			case ButtonState.DISABLED:
				targetScale = 1;
				break;
				
			case ButtonState.FOCUS:
				targetScale = 1.1f;
				break;
			}
		}
		
		void LeaveState(ButtonState nextState)
		{
		}
	}

}