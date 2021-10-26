using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Throwing Mechanics")]

	[Tooltip("If the player can currently throw.")]
	[SerializeField] private bool _canThrow = false;
	[Tooltip("Amount of time it takes to reach the landing zone.")]
	[SerializeField] private float _travelDuration = 1.0f;
	[Tooltip("The projectile that will be thrown.")]
	[SerializeField] private GameObject _projectile = null;
	[Tooltip("The point in which the projectile is fired from.")]
	[SerializeField] private Transform _throwPoint = null;
	[Tooltip("Minimum hold time before throwing is allowed.")]
	[SerializeField] private float _minimumMouseHoldTime = 0.1f;
	[Tooltip("The sprite used to show the landing zone of the projectile.")]
	[SerializeField] private GameObject _landingZoneSprite = null;
	[Tooltip("Which layers the player can throw on.")]
	[SerializeField] private LayerMask _layer = -1;
	[Tooltip("The Line Renderer that draws the projected parabola of the projectile.")]
	[SerializeField] private LineRenderer _lineRenderer = null;
	[Tooltip("Resolution of the drawn line.")]
	[SerializeField] private int _lineSegments = 10;

	private Vector3 _lastProjectileVelocity;



	protected void SetupPlayerThrow()
	{
		_canThrow = true;
		_lineRenderer.positionCount = _lineSegments;
	}

	protected void UpdateThrow()
	{
		if (LeftMouseDown && LeftMouseDownTime >= _minimumMouseHoldTime)
		{
			//Get ray from camera to mouse as a point;
			Ray screenToPointRay = Camera.main.ScreenPointToRay(CurrentMouse.position.ReadValue());
			//Create a raycasthit
			RaycastHit rayHit;

			//Check if the raycast intersects with an object on the desired layer
			if (Physics.Raycast(screenToPointRay, out rayHit, 100f, _layer))
			{
				//Move landing zone sprite to position
				_landingZoneSprite.transform.position = rayHit.point + Vector3.up * 0.1f;

				//Calculate the projectile velocity
				_lastProjectileVelocity = MathJ.CalculateProjectileVelocity(rayHit.point, _throwPoint.position, _travelDuration);

				//Visualize the trajectory
				VisualizeTrajectory(_lastProjectileVelocity);
			}
		}
	}

	protected void CheckThrowObject()
	{
		if (_canThrow && LeftMouseDownTime >= _minimumMouseHoldTime && CurrentMouse.leftButton.wasReleasedThisFrame)
		{
			ThrowObject();
		}
	}

	protected void ThrowObject()
	{

	}

	private void VisualizeTrajectory(Vector3 velocity)
	{

	}
}
