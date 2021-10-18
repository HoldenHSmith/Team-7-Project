using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRotate : MonoBehaviour
{
	[SerializeField] float m_RotationSpeed = 1;
	[SerializeField] float m_RotationAngleYStart = 45;
	[SerializeField] float m_RotationAngleYFinish = 45;

	private void Update()
	{
		float rY = Mathf.SmoothStep(m_RotationAngleYStart, m_RotationAngleYFinish, Mathf.PingPong(Time.time * m_RotationSpeed, 1));

		transform.rotation = Quaternion.Euler(0, rY, 0);
	}
}
