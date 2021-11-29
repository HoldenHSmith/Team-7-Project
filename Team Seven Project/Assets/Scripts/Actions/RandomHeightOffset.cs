using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeightOffset : MonoBehaviour
{
	[SerializeField] private float _minY = 0;
	[SerializeField] private float _maxY = 1;

	private void Awake()
	{
		Vector3 position = transform.position;
		position.y += Random.Range(_minY, _maxY);
		transform.position = position;
	}

}
