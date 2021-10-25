using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Throwing Mechanics")]

	[Tooltip("If the player can currently throw.")]
	[SerializeField] private bool _canThrow = false;
	[Tooltip("Amount of power given to a throw.")]
	[SerializeField] private float _throwPower = 1.0f;
	[Tooltip("The projectile that will be thrown.")]
	[SerializeField] private GameObject _projectile = null;
	[Tooltip("The point in which the projectile is fired from.")]
	[SerializeField] private Transform _throwPoint = null;
	[Tooltip("Minimum hold time before throwing is allowed.")]
	[SerializeField] private float _minimumMouseHoldTime = 0.1f;

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
