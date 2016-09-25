using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SS
{
	public class UICamera : MonoBehaviour
	{
		public static ArrayList	uiCameras = new ArrayList();
		public static UICamera	currUICam;

		Camera			currCam;
		GameObject		currObj;
		GameObject		pressObj;
		StringFlag		locker = new StringFlag();

		#region Locker
		public void Lock(string flag)
		{
			locker.AddFlag(flag);
			enabled = false;
		}

		public void Unlock(string flag)
		{
			locker.RemoveFlag(flag);
			if (locker.IsEmpty())
				enabled = true;
		}
		#endregion

		void Awake()
		{
			currCam = GetComponent<Camera>();
			currUICam = this;

			uiCameras.Add (this);
		}

		void Destroy()
		{
			uiCameras.Remove (this);
		}

		// Use this for initialization
		void Start ()
		{
		}
		
		// Update is called once per frame
		void Update ()
		{
			if (currCam == null)
			{
				currCam = GetComponent<Camera>();
			}
			if (currCam == null)
				return;
			
			object _ret = null;
			currObj = null;
			
			Vector2 screenPos = Input.mousePosition;
			
			// Construct a ray from the current mouse coordinates
			Ray ray = currCam.ScreenPointToRay (screenPos);
			RaycastHit raycastInfo;

			
			//Debug.DrawRay(ray.origin, ray.direction * 1000);
			if (Physics.Raycast (ray, out raycastInfo, 1000, ~gameObject.layer))
			{
				currObj = raycastInfo.collider.gameObject;
			}
			
			// PRESS true
			if (Input.GetMouseButtonDown(0))
			{
				if (currObj != null)
				{
					pressObj = currObj;
					EventManager.SendObjectEvent(currObj, "UICamera-Press", this, true, null, ref _ret, screenPos);
				}
			}
			// PRESS false (CLICK)
			else if (Input.GetMouseButtonUp(0))
			{
				if (currObj != null)
				{
					pressObj = null;
					EventManager.SendObjectEvent(currObj, "UICamera-Press", this, false, null, ref _ret, screenPos);
				}
			}
			// PRESS hold
			else if (Input.GetMouseButton(0))
			{
				if (currObj != pressObj)
				{
					if (pressObj != null)
					{
						EventManager.SendObjectEvent(pressObj, "UICamera-Leave", this, true, null, ref _ret, screenPos);
					}
					pressObj = currObj;
					if (currObj != null)
					{
						EventManager.SendObjectEvent(currObj, "UICamera-Press", this, true, null, ref _ret, screenPos);
					}
				}
				else
				{
					if (currObj != null)
					{
						EventManager.SendObjectEvent(currObj, "UICamera-Down", this, true, null, ref _ret, screenPos);
					}
				}
			}
			// Hover
			else
			{
				if (currObj != null)
				{
				}
				else
				{
					// Do not cast any object
					if (pressObj != null)
					{
						EventManager.SendObjectEvent(pressObj, "UICamera-Leave", this, true, null, ref _ret, screenPos);
						pressObj = currObj;
					}
				}
			}
		}
		
		#if UNITY_EDITOR
		/*
		void OnGUI()
		{
			GUILayout.Label("Current " + (currObj == null? "null":currObj.name));
			GUILayout.Label("Press " + (pressObj == null? "null":pressObj.name));
		}
		*/
		#endif
	}
	
}