using UnityEngine;
using static EnumsJ;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SimpleMove : MonoBehaviour
{
	
	[SerializeField] private LoopType m_LoopType;
	[SerializeField] private Vector3 m_StartPosition;
	[SerializeField] private Vector3 m_FinalPosition;
	[SerializeField] private float m_TravelTime;
	[SerializeField] private AnimationCurve m_AccelerationCurve;
	[SerializeField] private Rigidbody m_ObjectToMove;
	[SerializeField] private bool m_Active = false;

	[SerializeField, Range(0, 1)] public float previewPosition;

	private float m_Position = 0f;
	private float m_Time = 0f;
	private float m_Direction = 1f;

	public void Activate()
	{
		m_Active = true;
	}

	private void Update()
	{
		if (m_Active)
		{
			m_Time = m_Time + (m_Direction * Time.deltaTime / m_TravelTime);
			switch (m_LoopType)
			{
				case LoopType.Once:
					LoopOnce();
					break;
				case LoopType.PingPong:
					LoopPingPong();
					break;
				case LoopType.Repeat:
					LoopRepeat();
					break;
				default:
					break;
			}
			PerformTransform(m_Position);
		}
	}

	private void LoopOnce()
	{
		m_Position = Mathf.Clamp01(m_Time);
		if (m_Position >= 1)
		{
			m_Active = false;
			m_Direction *= -1;
		}
	}

	private void LoopPingPong()
	{
		m_Position = Mathf.PingPong(m_Time, 1f);
	}

	private void LoopRepeat()
	{
		m_Position = Mathf.Repeat(m_Time, 1f);
	}

	public void PerformTransform(float position)
	{

		float curvePosition = m_AccelerationCurve.Evaluate(position);
		Vector3 nextPosition = transform.TransformPoint(Vector3.Lerp(m_StartPosition, m_FinalPosition, curvePosition));

		if (Application.isEditor && !Application.isPlaying)
			m_ObjectToMove.transform.position = nextPosition;

		m_ObjectToMove.MovePosition(nextPosition);

	}

#if UNITY_EDITOR
	[CustomEditor(typeof(SimpleMove), true)]
	public class SimpleMoveEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			using (var cc = new EditorGUI.ChangeCheckScope())
			{
				base.OnInspectorGUI();
				if (cc.changed)
				{
					var pt = target as SimpleMove;
					pt.PerformTransform(pt.previewPosition);
					Debug.Log("Performing Transform");
				}
			}
		}

	}
#endif
}
