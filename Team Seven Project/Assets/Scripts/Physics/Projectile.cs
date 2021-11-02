using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
	[SerializeField] private Rigidbody _projectile = null;
	[SerializeField] private GameObject _landingZoneSprite = null;
	[SerializeField] private LayerMask _layer = -1;
	[SerializeField] private float _travelDuration = 0;
	[SerializeField] private Transform _startPoint = null;
	[SerializeField] private LineRenderer _lineRenderer = null;
	[SerializeField] private int _lineSegments = 10;

	private Vector3 _lastProjectileVelocity;
	private Camera _camera;

	//TEMP
	public Animator Animator;

	private void Awake()
	{
		_camera = Camera.main;
		_lineRenderer.positionCount = _lineSegments;
	}

	private void LaunchProjectile()
	{
		Ray camRay = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit hit;
		if (Physics.Raycast(camRay, out hit, 100f, _layer))
		{
			_landingZoneSprite.transform.position = hit.point + Vector3.up * 0.1f;

			_lastProjectileVelocity = MathJ.CalculateProjectileVelocity(hit.point, _startPoint.position, _travelDuration);
			Visualize(_lastProjectileVelocity);
			if (Mouse.current.leftButton.wasReleasedThisFrame)
			{
				Animator.Play("Throw");
			}

		}
	}

	public void SpawnProjectile()
	{
		Rigidbody obj = Instantiate(_projectile, _startPoint.position, Quaternion.identity);
		obj.velocity = _lastProjectileVelocity;
	}

	private void Visualize(Vector3 velocity)
	{
		for (int i = 0; i < _lineSegments; i++)
		{
			Vector3 pos = MathJ.CalculatePositionInTime(velocity, _startPoint.position, i / (float)_lineSegments);
			_lineRenderer.SetPosition(i, pos);
		}
	}

	private void Update()
	{
		LaunchProjectile();
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			_lineRenderer.enabled = true;
			_landingZoneSprite.SetActive(true);
		}
		else if (Mouse.current.leftButton.wasReleasedThisFrame)
		{
			_lineRenderer.enabled = false;
			_landingZoneSprite.SetActive(false);
		}
	}

}
