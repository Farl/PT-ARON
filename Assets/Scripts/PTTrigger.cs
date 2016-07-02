using UnityEngine;
using System.Collections;

public class PTTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos()
	{
		Collider[] colliders;
		colliders = GetComponents<Collider> ();

		foreach (Collider c in colliders) {
			if (c.GetType() == typeof(BoxCollider)) {
				BoxCollider bc = (BoxCollider)c;
				Gizmos.color = new Color (0, 1, 0, 0.5f);
				Gizmos.DrawWireCube (transform.position + bc.center, bc.size);
				Gizmos.color = new Color (0, 1, 0, 0.25f);
				Gizmos.DrawCube (transform.position + bc.center, bc.size);
			}
			else if (c.GetType() == typeof(SphereCollider)) {
				SphereCollider sc = (SphereCollider)c;
				Gizmos.color = new Color (0, 1, 0, 0.5f);
				Gizmos.DrawWireSphere (transform.position + sc.center, sc.radius);
				Gizmos.color = new Color (0, 1, 0, 0.25f);
				Gizmos.DrawSphere (transform.position + sc.center, sc.radius);
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Collider[] colliders;
		colliders = GetComponents<Collider> ();

		foreach (Collider c in colliders) {
			if (c.GetType() == typeof(BoxCollider)) {
				BoxCollider bc = (BoxCollider)c;
				Gizmos.color = new Color (0, 1, 0, 1);
				Gizmos.DrawWireCube (transform.position + bc.center, bc.size);
				Gizmos.color = new Color (0, 1, 0, 0.25f);
				Gizmos.DrawCube (transform.position + bc.center, bc.size);
			}
			else if (c.GetType() == typeof(SphereCollider)) {
				SphereCollider sc = (SphereCollider)c;
				Gizmos.color = new Color (0, 1, 0, 0.5f);
				Gizmos.DrawWireSphere (transform.position + sc.center, sc.radius);
				Gizmos.color = new Color (0, 1, 0, 0.25f);
				Gizmos.DrawSphere (transform.position + sc.center, sc.radius);
			}
		}
	}
}
