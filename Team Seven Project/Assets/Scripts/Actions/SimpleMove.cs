using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimpleMove : MonoBehaviour, IMessageReceiver
{

	[SerializeField] private LoopType _loopType = LoopType.Repeat;
	[SerializeField] private Vector3 _startPosition = Vector3.zero;
	[SerializeField] private Vector3 _finalPosition = Vector3.zero;
	[SerializeField] private float _travelTime = 1.0f;
	[SerializeField] private AnimationCurve _accelerationCurve = null;
	[SerializeField] private Rigidbody _objectToMove = null;
	public bool _active = false;

	[SerializeField, Range(0, 1)] public float PreviewPosition;

	private float _position = 0f;
	private float _time = 0f;
	private float _direction = 1f;

	public void Activate()
	{
		_active = true;
	}

	private void Update()
	{
		if (_active)
		{
			_time = _time + (_direction * Time.deltaTime / _travelTime);
			switch (_loopType)
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
			PerformTransform(_position);
		}
	}

	private void LoopOnce()
	{
		_position = Mathf.Clamp01(_time);
		if (_position >= 1)
		{
			_active = false;
			_direction *= -1;
		}
	}

	private void LoopPingPong()
	{
		_position = Mathf.PingPong(_time, 1f);
	}

	private void LoopRepeat()
	{
		_position = Mathf.Repeat(_time, 1f);
	}

	public void PerformTransform(float position)
	{

		float curvePosition = _accelerationCurve.Evaluate(position);
		Vector3 nextPosition = transform.TransformPoint(Vector3.Lerp(_startPosition, _finalPosition, curvePosition));

		if (Application.isEditor && !Application.isPlaying)
			_objectToMove.transform.position = nextPosition;

		_objectToMove.MovePosition(nextPosition);

	}

	public bool ReceiveMessage(Telegram message)
	{
		if (message.MessageType == MessageType.Msg_Activate)
		{
			Activate();
			return true;
		}

		return false;
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
					pt.PerformTransform(pt.PreviewPosition);
				}
			}
		}

	}
#endif
}
