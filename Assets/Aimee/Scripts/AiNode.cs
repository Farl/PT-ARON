using UnityEngine;
using System.Collections;

public class AiNode : MonoBehaviour {

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawCube (Vector3.zero, Vector3.one * 0.1f);
	}
}
