using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
	[SerializeField] private Rigidbody m_Projectile;
	[SerializeField] private GameObject m_LandingZoneSprite;
	[SerializeField] private LayerMask m_Layer;
	[SerializeField] private float m_TravelDuration;
	[SerializeField] private Transform m_StartPoint;
	[SerializeField] private LineRenderer m_LineRenderer;
	[SerializeField] private int m_LineSegments = 10;

	private Vector3 m_LastProjectileVelocity;
	private Camera m_Camera;
	//TEMP
	public Animator animator;
	private void Awake()
	{
		m_Camera = Camera.main;
		m_LineRenderer.positionCount = m_LineSegments;
	}

	private void LaunchProjectile()
	{
		Ray camRay = m_Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit hit;
		if (Physics.Raycast(camRay, out hit, 100f, m_Layer))
		{
			m_LandingZoneSprite.SetActive(true);
			//Debug.Log(Mouse.current.position.ReadValue());
			m_LandingZoneSprite.transform.position = hit.point + Vector3.up * 0.1f;

			m_LastProjectileVelocity = MathJ.CalculateProjectileVelocity(hit.point, m_StartPoint.position, m_TravelDuration);
			Visualize(m_LastProjectileVelocity);
			if (Mouse.current.leftButton.wasReleasedThisFrame)
			{
				animator.Play("Throw");
			}
			
		}
	}
	public void SpawnProjectile()
	{
		Rigidbody obj = Instantiate(m_Projectile, m_StartPoint.position, Quaternion.identity);
		obj.velocity = m_LastProjectileVelocity;
	}
	private void Visualize(Vector3 velocity)
	{
		for(int i = 0; i < m_LineSegments;i++)
		{
			Vector3 pos = MathJ.CalculatePositionInTime(velocity, m_StartPoint.position, i / (float)m_LineSegments);
			m_LineRenderer.SetPosition(i, pos);
		}
	}

	private void Update()
	{
		LaunchProjectile();
	}


}
