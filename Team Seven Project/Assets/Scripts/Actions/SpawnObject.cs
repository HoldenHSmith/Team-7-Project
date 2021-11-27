using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
	[SerializeField] private GameObject _objectToSpawn = null;

	public void CallSpawnObject()
	{
		GameObject go = Instantiate(_objectToSpawn);
		go.transform.position = transform.position;
	}
}
