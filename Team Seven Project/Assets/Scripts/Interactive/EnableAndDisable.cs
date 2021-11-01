using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndDisable : MonoBehaviour
{
	[SerializeField] private GameObject _object = null;
	[SerializeField] private bool _activeOnSpawn = false;

	private void Awake()
	{
		if (_object != null)
			_object.SetActive(_activeOnSpawn);
	}

	public void Activate()
	{
		if (_object != null)
			_object.SetActive(true);
	}

	public void Deactivate()
	{
		if (_object != null)
			_object.SetActive(false);
	}
}
