using UnityEngine;

public class ThrowController : MonoBehaviour
{
	[SerializeField] private float _rotationSpeed;
	[SerializeField] private float _throwPower;

	[SerializeField] private GameObject _projectile;
	[SerializeField] private Transform _throwPoint;

	public void ThrowObject()
	{
		GameObject spawnedProjectile = Instantiate(_projectile, _throwPoint.position, _throwPoint.rotation);
		spawnedProjectile.GetComponent<Rigidbody>().velocity = _throwPoint.transform.up * _throwPower;
	}
}
