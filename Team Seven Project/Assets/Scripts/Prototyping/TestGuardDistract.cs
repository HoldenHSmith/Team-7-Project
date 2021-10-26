using UnityEngine.AI;
using UnityEngine;

public class TestGuardDistract : MonoBehaviour
{
	private NavMeshAgent _navMesh;
	private EnemyAlertState _alertness;
	protected Animator Animator;
	private int _walkHash;
	private Vector3 _destination;

	void Awake()
	{
		_navMesh = GetComponent<NavMeshAgent>();
		_alertness = GetComponent<EnemyAlertState>();
		Animator = GetComponentInChildren<Animator>();
		_walkHash = Animator.StringToHash("Walking");
		_destination = transform.position;
	}

	private void Update()
	{
		Animator.SetBool(_walkHash, Vector3.Distance(transform.position, _destination) > 0.25f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Projectile")
		{
			Debug.Log("ALERT!");
			_alertness.SetAlertLevel(EnemyAlertState.AlertLevel.Investigating);
			_destination = other.transform.position;
			_destination.y = 0;
			_navMesh.SetDestination(_destination);

		}
	}

}
