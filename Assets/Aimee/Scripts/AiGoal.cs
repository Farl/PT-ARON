using UnityEngine;
using System.Collections;

public class AiGoal : MonoBehaviour {

	void OnTriggerEnter(Collider coll)
	{
		AiCharacter ch = coll.GetComponent<AiCharacter> ();
		if (ch && ch.type == AiCharacter.AiCharacterType.Player) {
			ch.Win ();
		}
	}
}
