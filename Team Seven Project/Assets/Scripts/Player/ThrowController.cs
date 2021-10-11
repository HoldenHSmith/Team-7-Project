using UnityEngine;

public class ThrowController : MonoBehaviour
{
	[SerializeField] private float m_RotationSpeed;
	[SerializeField] private float m_ThrowPower;

	[SerializeField] private GameObject m_Projectile;
	[SerializeField] private Transform m_ThrowPoint;

	public void ThrowObject()
	{
		GameObject spawnedProjectile = Instantiate(m_Projectile, m_ThrowPoint.position, m_ThrowPoint.rotation);
		spawnedProjectile.GetComponent<Rigidbody>().velocity = m_ThrowPoint.transform.up * m_ThrowPower;
	}
}
