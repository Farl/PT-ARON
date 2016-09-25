using UnityEngine;
using System.Collections;
using SS;

public class EventToSpawner : EventListener {

	public string m_spawnResource;
	public GameObject m_spawnObj;

	GameObject spawnObj;

	protected override void OnEvent()
	{
		if (spawnObj != null)
			return;

		if (m_spawnResource != null && m_spawnResource != "")
			spawnObj = (GameObject)GameObject.Instantiate(Resources.Load (m_spawnResource), transform.position, transform.rotation);
		if (m_spawnObj != null)
			spawnObj = (GameObject)GameObject.Instantiate(m_spawnObj, transform.position, transform.rotation);
	}
}
