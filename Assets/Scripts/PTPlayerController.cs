using UnityEngine;
using System.Collections;

public class PTPlayerController : PTController {
	public Camera mainCamera;
	public LayerMask groundLayer;
	public GameObject followDummy;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		targetVec = Vector3.zero;
		targetObj = null;

		if (mainCamera) {
			if (Input.GetMouseButton (0)) {
				Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitInfo;
				if (Physics.Raycast (ray, out hitInfo, 1000, groundLayer.value)) {

					if (followDummy == null) {
						followDummy = new GameObject ("Follow-Dummy");
					}
					if (followDummy != null) {
						followDummy.transform.position = hitInfo.point;
					}
				}
			}
		}

		if (followDummy) {
			if ((followDummy.transform.position - transform.position).magnitude < 0.02f) {
				Destroy (followDummy);
			} else {
				targetVec = followDummy.transform.position - transform.position;
				targetObj = followDummy;
			}
		}
	}
}
