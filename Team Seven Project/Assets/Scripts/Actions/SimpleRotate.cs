using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRotate : MonoBehaviour
{
	[SerializeField] private float _rotationSpeed = 0.1f;
	[SerializeField] private bool _active = true;

	private float _rotationAngleYStart = -45;
	private float _rotationAngleYFinish = 45;

	private Vector3 _startEuler;

	private void Awake()
	{
		_startEuler = transform.rotation.eulerAngles;
	}

	private void Update()
	{
		float rY = Mathf.SmoothStep(_rotationAngleYStart, _rotationAngleYFinish, Mathf.PingPong(Time.time * _rotationSpeed, 1));

		transform.rotation = Quaternion.Euler(_startEuler.x, rY, _startEuler.z);
	}

	public void SetRotationValues(Vector2 rotationValues)
	{
		_rotationAngleYStart = rotationValues.x;
		_rotationAngleYFinish = rotationValues.y;
	}
}
