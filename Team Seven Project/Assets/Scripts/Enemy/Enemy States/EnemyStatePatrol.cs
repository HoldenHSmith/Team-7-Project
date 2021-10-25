using UnityEngine;
using UnityEngine.AI;

public class EnemyStatePatrol : EnemyState
{
	private WaypointManager _waypointManager;
	private float _waitTime = 0f;
	private Waypoint _currentWaypoint;
	private NavMeshAgent _navMeshAgent;

	public EnemyStatePatrol(StateMachine stateMachine, Enemy enemy, WaypointManager waypointManager) : base(stateMachine, enemy)
	{
		_waypointManager = Enemy.GetComponent<WaypointManager>();
		_navMeshAgent = Enemy.GetComponent<NavMeshAgent>();
	}

	public override void OnEnter()
	{
		if (!GetNextWaypoint())
		{
			StateMachine.RequestStateChange(Enemy.EnemyStates.StateIdle);
			return;
		}
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate()
	{
		//Get Distance to Current Waypoint
		float distance = Vector3.Distance(Enemy.transform.position, _currentWaypoint.Position);

		//If we aree within range of the waypoint...
		if (distance <= Enemy.Settings.DistanceToWaypointSatisfaction)
		{
			//...Check that we have satisfied the Wait Time
			if (_waitTime <= 0)
			{
				//...Get the next waypoint
				if (!GetNextWaypoint())
				{
					StateMachine.RequestStateChange(Enemy.EnemyStates.StateIdle);
					return;
				}
			}
			else
			{
				//...Reduce the Wait Time
				_waitTime -= Time.deltaTime;
			}
		}

		//Set Enemy's walking animation
		Enemy.SetWalkAnimation(!Mathf.Approximately(_navMeshAgent.velocity.magnitude, 0));
	}

	private bool GetNextWaypoint()
	{
		if (_waypointManager.GetNextWaypoint(out _currentWaypoint))
		{
			_waitTime = _currentWaypoint.WaitTime;
			_navMeshAgent.SetDestination(_currentWaypoint.Position);
			return true;
		}

		return false;
	}
}
