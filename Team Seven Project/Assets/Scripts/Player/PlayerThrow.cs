using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Throwing Mechanics")]

	[Tooltip("If the player can currently throw.")]
	[SerializeField] private bool _canThrow;
	[Tooltip("Amount of power given to a throw.")]
	[SerializeField] private float _throwPower;
	[Tooltip("The projectile that will be thrown.")]
	[SerializeField] private GameObject _projectile;
	[Tooltip("The point in which the projectile is fired from.")]
	[SerializeField] private Transform _throwPoint;
	[Tooltip("Minimum hold time before throwing is allowed.")]
	[SerializeField] private float _minimumMouseHoldTime;

	protected void SetupThrow()
	{
		_canThrow = true;
	}

	protected void UpdateThrow()
	{
		if(CurrentMouse.leftButton.wasReleasedThisFrame)
		{
			CheckThrowObject();
		}
	}

	protected void CheckThrowObject()
	{
		if (_canThrow && LeftMouseDownTime >= _minimumMouseHoldTime && CurrentMouse.leftButton.wasReleasedThisFrame)
		{
			//ThrowObject();
		}
	}

	protected void ThrowObject()
	{
		GameObject spawnedProjectile = Instantiate(_projectile, _throwPoint.position, _throwPoint.rotation);
		spawnedProjectile.GetComponent<Rigidbody>().velocity = _throwPoint.transform.up * _throwPower;
	}
}
