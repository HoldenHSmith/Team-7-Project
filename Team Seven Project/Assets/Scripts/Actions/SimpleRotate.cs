using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRotate : MonoBehaviour
{
	[SerializeField] private float _rotationSpeed = 1;
	[SerializeField] private float _rotationAngleYStart = 45;
	[SerializeField] private float _rotationAngleYFinish = 45;

	private void Update()
	{
		float rY = Mathf.SmoothStep(_rotationAngleYStart, _rotationAngleYFinish, Mathf.PingPong(Time.time * _rotationSpeed, 1));

		transform.rotation = Quaternion.Euler(0, rY, 0);
	}
}
