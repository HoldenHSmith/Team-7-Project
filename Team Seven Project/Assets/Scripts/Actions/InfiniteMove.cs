using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMove : MonoBehaviour
{
	[SerializeField] private Vector3 _direction;
	[SerializeField] private float _speed;
	[SerializeField] private Vector3 _resetPosition;
	[SerializeField] private float _distance;

	private void Update()
	{
		float distance = Vector3.Distance(transform.position, _resetPosition);

		if (distance >= _distance)
		{
			transform.position = _resetPosition;
		}

		transform.position += _direction * _speed * Time.deltaTime;
	}
}
