using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Throwing Mechanics")]

	[Tooltip("If the player can currently throw.")]
	[SerializeField] private bool _throwEnabled = false;

	[Tooltip("Whether player has a beaker or not.")]
	[SerializeField] private bool _hasBeaker = false;

	[Tooltip("Amount of time it takes to reach the landing zone.")]
	[SerializeField] private float _travelDuration = 1.0f;

	[Tooltip("The projectile that will be thrown.")]
	[SerializeField] private Rigidbody _projectile = null;

	[Tooltip("The point in which the projectile is fired from.")]
	[SerializeField] private Transform _throwPoint = null;

	[Tooltip("Minimum hold time before throwing is allowed.")]
	[SerializeField] private float _minimumMouseHoldTime = 0.1f;

	[Tooltip("The sprite used to show the landing zone of the projectile.")]
	[SerializeField] private GameObject _landingZoneSprite = null;

	[Tooltip("Which layers the player can throw on.")]
	[SerializeField] private LayerMask _throwLayer = 0;

	[Tooltip("The Line Renderer that draws the projected parabola of the projectile.")]
	[SerializeField] private LineRenderer _lineRenderer = null;

	[Tooltip("Resolution of the drawn line.")]
	[SerializeField] private int _lineSegments = 10;

	private Vector3 _lastProjectileVelocity;
	private bool _validThrow = false;


	protected void SetupPlayerThrow()
	{
		_throwEnabled = true;
		_lineRenderer.positionCount = _lineSegments;
	}

	protected void UpdateThrow()
	{
		if (_throwEnabled && _hasBeaker &&LeftMouseDown && LeftMouseDownTime >= _minimumMouseHoldTime)
		{
			//Get ray from camera to mouse as a point;
			Ray screenToPointRay = Camera.main.ScreenPointToRay(CurrentMouse.position.ReadValue());
			//Create a raycasthit
			RaycastHit rayHit;

			//Check if the raycast intersects with an object on the desired layer
			if (Physics.Raycast(screenToPointRay, out rayHit, 100f, _throwLayer))
			{
				//Move landing zone sprite to position
				_landingZoneSprite.transform.position = rayHit.point + (rayHit.normal * 0.1f);
				_landingZoneSprite.transform.rotation = Quaternion.FromToRotation(Vector3.forward, rayHit.normal);
				//Calculate the projectile velocity
				_lastProjectileVelocity = MathJ.CalculateProjectileVelocity(rayHit.point, _throwPoint.position, _travelDuration);

				//Visualize the trajectory
				EnableThrowVisuals();
				VisualizeTrajectory(_lastProjectileVelocity);
				_validThrow = true;
			}
			else
			{
				DisableThrowVisuals();
				_validThrow = false;
			}


		}
		else
		{
			DisableThrowVisuals();
		}

		if (_validThrow && CurrentMouse.leftButton.wasReleasedThisFrame && _hasBeaker)
		{
			ThrowObject();
			_hasBeaker = false;
		}

	}

	protected void ThrowObject()
	{
		Animator.Play("Throw");
	}

	public void SpawnProjectile()
	{
		Rigidbody obj = Instantiate(_projectile, _throwPoint.position, Quaternion.identity);
		obj.velocity = _lastProjectileVelocity;
	}

	private void DisableThrowVisuals()
	{
		_lineRenderer.enabled = false;
		_landingZoneSprite.SetActive(false);
	}

	private void EnableThrowVisuals()
	{
		_lineRenderer.enabled = true;
		_landingZoneSprite.SetActive(true);
	}

	private void VisualizeTrajectory(Vector3 velocity)
	{

		for (int i = 0; i < _lineSegments; i++)
		{
			Vector3 position = MathJ.CalculatePositionInTime(velocity, _throwPoint.position, i / (float)_lineSegments);
			_lineRenderer.SetPosition(i, position);
		}
	}

	//Properties
	public bool HasBeaker { get => HasBeaker; set => _hasBeaker = value; }
}
