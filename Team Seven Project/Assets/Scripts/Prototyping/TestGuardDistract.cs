using UnityEngine.AI;
using UnityEngine;

public class TestGuardDistract : MonoBehaviour
{
	private NavMeshAgent m_NavMesh;
	private EnemyAlertState m_Alertness;
	protected Animator animator;
	private int walkHash;
	private Vector3 destination;
	// Start is called before the first frame update
	void Awake()
    {
		m_NavMesh = GetComponent<NavMeshAgent>();
		m_Alertness = GetComponent<EnemyAlertState>();
		animator = GetComponentInChildren<Animator>();
		walkHash = Animator.StringToHash("Walking");
		destination = transform.position;
	}

	private void Update()
	{
		
		animator.SetBool(walkHash, Vector3.Distance(transform.position,destination) > 0.25f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Projectile")
		{
			Debug.Log("ALERT!");
			m_Alertness.SetAlertLevel(EnemyAlertState.AlertLevel.Investigating);
			destination = other.transform.position;
			destination.y = 0;
			m_NavMesh.SetDestination(destination);

		}
	}

}
