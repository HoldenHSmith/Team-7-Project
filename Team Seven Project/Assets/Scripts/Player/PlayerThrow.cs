using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Throwing Mechanics")]

	[Tooltip("If the player can currently throw.")]
	[SerializeField] private bool m_CanThrow;
	[Tooltip("Amount of power given to a throw.")]
	[SerializeField] private float m_ThrowPower;
	[Tooltip("The projectile that will be thrown.")]
	[SerializeField] private GameObject m_Projectile;
	[Tooltip("The point in which the projectile is fired from.")]
	[SerializeField] private Transform m_ThrowPoint;
	[Tooltip("Minimum hold time before throwing is allowed.")]
	[SerializeField] private float m_MinimumMouseHoldTime;

	protected void SetupThrow()
	{
		m_CanThrow = true;
	}

	protected void UpdateThrow()
	{
		if(m_CurrentMouse.leftButton.wasReleasedThisFrame)
		{
			CheckThrowObject();
		}
	}

	protected void CheckThrowObject()
	{
		if (m_CanThrow && m_LeftMouseDownTime >= m_MinimumMouseHoldTime && m_CurrentMouse.leftButton.wasReleasedThisFrame)
		{
			//ThrowObject();
		}
	}

	protected void ThrowObject()
	{
		GameObject spawnedProjectile = Instantiate(m_Projectile, m_ThrowPoint.position, m_ThrowPoint.rotation);
		spawnedProjectile.GetComponent<Rigidbody>().velocity = m_ThrowPoint.transform.up * m_ThrowPower;
	}
}
