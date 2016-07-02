using UnityEngine;
using System.Collections;

public class AiCharacter : MonoBehaviour {

	public enum AiCharacterType
	{
		Player,
		Enemy,
		Invalid
	}
	public AiCharacterType type;
	public float speed = 1.0f;
	public bool isMoving = false;
	private AiNode targetNode;
	private AiCharacter interestCharacter;
	private AiGame aiGame;
	public LayerMask layerIgnore;

	public void SetGame(AiGame _aiGame)
	{
		aiGame = _aiGame;
	}

	public AiGame GetGame()
	{
		return aiGame;
	}

	public void Win()
	{
		if (aiGame) {
			aiGame.Win ();
		}
	}

	public void GameOver()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb) {
			rb.constraints = RigidbodyConstraints.None;
			rb.mass = 0.1f;
		}
		type = AiCharacterType.Invalid;
		if (aiGame) {
			aiGame.GameOver ();
		}
	}

	public bool MoveTo(AiNode n, float gridSize)
	{
		RaycastHit hitInfo;
		if (Physics.Raycast (new Ray (transform.position, n.transform.position - transform.position), out hitInfo, gridSize, LayerMask.GetMask("Blocker") ) ) {
			Debug.Log (hitInfo.collider.gameObject.name);
			return false;
		} else {
			targetNode = n;
			transform.LookAt (n.transform);
			isMoving = true;
		}
		return true;
	}

	void Update()
	{
		if (targetNode) {
			transform.position = Vector3.MoveTowards (transform.position, targetNode.transform.position, speed * Time.deltaTime);
			float currDist = (transform.position - targetNode.transform.position).magnitude;
			if (currDist < float.Epsilon) {
				targetNode = null;
				isMoving = false;
				if (aiGame) {
					aiGame.PlayerDone ();
				}
			}
		}
	}

	public void UpdateAI(AiCharacter player, AiNode[] nodes, float gridSize)
	{
		if (aiGame) {
			if (interestCharacter) {
				AiNode n = aiGame.FindNode (interestCharacter.transform.position);
				if (n) {
					MoveTo (n, gridSize);
					interestCharacter = null;
				}
			}
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		if (type == AiCharacterType.Enemy) {
			AiCharacter ch = coll.gameObject.GetComponent<AiCharacter> ();
			if (ch && ch.type == AiCharacterType.Player) {
				if (Vector3.Angle (ch.transform.position - transform.position, -transform.forward) < 100) {
					Destroy (gameObject);
				} else {
					ch.GameOver ();
				}
			}
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (type == AiCharacterType.Enemy) {
			AiCharacter ch = coll.gameObject.GetComponent<AiCharacter> ();
			if (ch && ch.type == AiCharacterType.Player) {
				interestCharacter = ch;
			}
		}
	}
}
