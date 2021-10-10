using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
	[SerializeField] private Vector3 m_StartPosition;
	[SerializeField] private Vector3 m_FinalPosition;
	[SerializeField] private float m_TravelTime;
	[SerializeField] private AnimationCurve m_AccelerationCurve;
	[SerializeField] private Rigidbody m_ObjectToMove;

	private float m_Position = 0f;
	private float m_Time = 0f;
	private float m_Direction = 1f;
	private bool m_Active = false;

	public void Activate()
	{
		m_Active = true;
	}

	private void Update()
	{
		if (m_Active)
		{
			m_Time = m_Time + (m_Direction * Time.deltaTime / m_TravelTime);

			LoopOnce();
			PerformTransform(m_Position);
		}
	}

	private void LoopOnce()
	{
		m_Position = Mathf.Clamp01(m_Time);
		if(m_Position >= 1)
		{
			m_Active = false;
			m_Direction *= -1;
		}
	}

	public void PerformTransform(float position)
	{

		float curvePosition = m_AccelerationCurve.Evaluate(position);
		Vector3 nextPosition = transform.TransformPoint(Vector3.Lerp(m_StartPosition, m_FinalPosition, curvePosition));

		m_ObjectToMove.MovePosition(nextPosition);

	}


}
