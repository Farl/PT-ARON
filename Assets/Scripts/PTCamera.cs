using UnityEngine;
using System.Collections;

public class PTCamera : MonoBehaviour {
	public static PTCamera main;
	public Camera camera;

	public Vector3 followAngle;
	public float followDist;
	public Transform targetTrans;

	enum Type
	{
		FOLLOW,
		SECURITY,
	}
	private Type type = Type.FOLLOW;

	// Use this for initialization
	void Start () {
		main = this;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCamera ();
	}

	void UpdateCamera()
	{
		switch (type) {
		case Type.FOLLOW:
			if (targetTrans) {
				Vector3 destPos = targetTrans.position;
				Vector3 offsetVec = Quaternion.Euler(followAngle) * new Vector3 (0, 0, followDist);
				destPos -= offsetVec;

				transform.position = destPos;
				transform.LookAt (targetTrans);
			}
			break;
		case Type.SECURITY:
			break;
		}
	}
}
