using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class AiGame : MonoBehaviour {

	private AiNode[] nodes;
	public GameObject nodeParent;
	public GameObject aiParent;
	public AiCharacter player;
	private AiCharacter[] aiCharacters;
	public float gridSize = 2;
	public GameObject gameOverPanel;
	public GameObject controlPanel;
	private bool playerTurn = true;

	private int direction = 0;

	public void SetPlayerDirection(int dir)
	{
		direction = dir;
	}

	// Use this for initialization
	void Start () {
		if (nodeParent) {
			nodes = nodeParent.GetComponentsInChildren<AiNode> ();
		}
		if (player) {
			player.SetGame (this);
		}
		if (aiParent) {
			aiCharacters = aiParent.GetComponentsInChildren<AiCharacter> ();
		}
		if (aiCharacters != null) {
			foreach (AiCharacter ch in aiCharacters) {
				ch.SetGame (this);
			}
		}
		if (gameOverPanel) {
			gameOverPanel.SetActive (false);
		}
	}

	public void PlayerDone()
	{
		if (playerTurn) {
			playerTurn = !playerTurn;
			if (aiCharacters != null) {
				foreach (AiCharacter ch in aiCharacters) {
					if (ch) {
						ch.UpdateAI (player, nodes, gridSize);
					}
				}
			}
		}
	}

	public void Win()
	{
		if (SceneManager.sceneCount > 0) {
			SceneManager.LoadScene ((SceneManager.GetActiveScene ().buildIndex + 1) % (SceneManager.sceneCountInBuildSettings));
		} else {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	public void GameOver()
	{
		StartCoroutine (GameOverCoroutine ());
	}

	IEnumerator GameOverCoroutine()
	{
		if (gameOverPanel) {
			gameOverPanel.SetActive (true);
		}
		yield return new WaitForSeconds (2.0f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public AiNode FindNode(Vector3 predictPos)
	{
		foreach (AiNode n in nodes) {
			float distance = (predictPos - n.transform.position).magnitude;
			if (distance < gridSize * 0.5f) {
				return n;
			}
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player) {

			if (gameOverPanel && gameOverPanel.activeInHierarchy) {
				return;
			}
			bool isMoving = false;
			if (playerTurn && !player.isMoving) {
				Vector3 predictPos = player.transform.position;
				if (direction == 1 || Input.GetKeyDown (KeyCode.UpArrow)) {
					predictPos += new Vector3(0, 0, gridSize);
					isMoving = true;
				}
				else if (direction == 2 || Input.GetKeyDown (KeyCode.DownArrow)) {
					predictPos += new Vector3(0, 0, -gridSize);
					isMoving = true;
				}
				else if (direction == 3 || Input.GetKeyDown (KeyCode.LeftArrow)) {
					predictPos += new Vector3(-gridSize, 0, 0);
					isMoving = true;
				}
				else if (direction == 4 || Input.GetKeyDown (KeyCode.RightArrow)) {
					predictPos += new Vector3(gridSize, 0, 0);
					isMoving = true;
				}

				direction = 0;

				if (isMoving) {
					AiNode n = FindNode (predictPos);
					if (n) {
						player.MoveTo (n, gridSize);
					}
				}
			} else if (!playerTurn) {
				if (aiCharacters != null) {
					foreach (AiCharacter ch in aiCharacters) {
						isMoving |= ch.isMoving;
					}
				}
				if (!isMoving) {
					playerTurn = !playerTurn;
				}
			}
		}
	}
}
