using UnityEngine;

public class ThrowController : MonoBehaviour
{
	[SerializeField] private float _throwPower = 1.0f;

	[SerializeField] private GameObject _projectile = null;
	[SerializeField] private Transform _throwPoint = null;

	public void ThrowObject()
	{
		GameObject spawnedProjectile = Instantiate(_projectile, _throwPoint.position, _throwPoint.rotation);
		spawnedProjectile.GetComponent<Rigidbody>().velocity = _throwPoint.transform.up * _throwPower;
	}
}
