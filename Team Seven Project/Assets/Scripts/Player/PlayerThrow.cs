﻿using UnityEngine;

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

	private Vector3 _finalPosition = Vector3.zero;

	protected void SetupPlayerThrow()
	{
		_throwEnabled = true;
		_lineRenderer.positionCount = _lineSegments;
		if (SaveManager.Instance.Current != null)
			_hasBeaker = SaveManager.Instance.Current.HasBeaker;
	}

	protected void UpdateThrow()
	{
		if (_throwEnabled && _hasBeaker && _leftMouseDown && _leftMouseDownTime >= _minimumMouseHoldTime)
		{
			//Get ray from camera to mouse as a point;
			Ray screenToPointRay = Camera.main.ScreenPointToRay(_currentMouse.position.ReadValue());
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
				_finalPosition = rayHit.point;
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

		if (_validThrow && _currentMouse.leftButton.wasReleasedThisFrame && _hasBeaker)
		{
			ThrowObject();
			_hasBeaker = false;
		}

	}

	protected void ThrowObject()
	{
		_animator.Play("Throw");
	}

	public void SpawnProjectile()
	{
		Rigidbody obj = Instantiate(_projectile, _throwPoint.position, Quaternion.identity);
		obj.velocity = MathJ.CalculateProjectileVelocity(_finalPosition, _throwPoint.position, _travelDuration);
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
	public bool HasBeaker { get => _hasBeaker; set => _hasBeaker = value; }

}

