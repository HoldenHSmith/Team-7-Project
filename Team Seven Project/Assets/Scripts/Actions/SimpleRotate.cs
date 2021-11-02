using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
	[SerializeField] private Transform _objectTransform = null;
	[SerializeField] private float _rotationSpeed = 0.1f;
	[SerializeField] private bool _active = true;


	private float _rotationAngleYStart = -45;
	private float _rotationAngleYFinish = 45;

	private Vector3 _startEuler;

	private void Awake()
	{
		_startEuler = _objectTransform.rotation.eulerAngles;
	}

	private void Update()
	{
		if (_active)
		{
			float rY = Mathf.SmoothStep(_rotationAngleYStart, _rotationAngleYFinish, Mathf.PingPong(Time.time * _rotationSpeed, 1));

			_objectTransform.rotation = Quaternion.Euler(_startEuler.x, rY, _startEuler.z);
		}
	}

	public void SetRotationValues(Vector2 rotationValues)
	{
		_rotationAngleYStart = rotationValues.x;
		_rotationAngleYFinish = rotationValues.y;
	}
}
