using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Throwing Mechanics")]

	[SerializeField] private bool m_CanThrow;
	[SerializeField] private float m_ThrowPower;

	[SerializeField] private GameObject m_Projectile;
	[SerializeField] private Transform m_ThrowPoint;
	[SerializeField] private float m_MinimumMouseHoldTime;


	protected void UpdateThrow()
	{
		

	}

	protected void CheckThrowObject()
	{
		if(m_CanThrow && m_LeftMouseDownTime >= m_MinimumMouseHoldTime)
		{
			ThrowObject();
		}
	}

	protected void ThrowObject()
	{
		GameObject spawnedProjectile = Instantiate(m_Projectile, m_ThrowPoint.position, m_ThrowPoint.rotation);
		spawnedProjectile.GetComponent<Rigidbody>().velocity = m_ThrowPoint.transform.up * m_ThrowPower;
	}
}
