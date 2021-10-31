using UnityEngine;

[RequireComponent(typeof(SimpleRotate))]
public class GuardCamera : MonoBehaviour
{
	[SerializeField] private GameObject _pivotPoint;
	[SerializeField] private Vector3 _pivotRotation;
	[SerializeField] private Vector2 _ToFromRotation;

	private void OnValidate()
	{
		if (_pivotPoint != null)
		{
			_pivotPoint.transform.eulerAngles = _pivotRotation;

			if (TryGetComponent(out SimpleRotate rot))
			{
				rot.SetRotationValues(new Vector2(_pivotRotation.y + _ToFromRotation.x, _pivotRotation.y + _ToFromRotation.y));
			}
		}

	}


}
