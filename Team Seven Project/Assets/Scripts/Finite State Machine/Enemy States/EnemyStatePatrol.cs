using UnityEngine;
using UnityEngine.AI;

public class EnemyStatePatrol : EnemyState
{
	private WaypointManager m_WaypointManager;
	private float m_WaitTime = 0f;
	private Waypoint m_CurrentWaypoint;
	private NavMeshAgent m_NavMeshAgent;

	public EnemyStatePatrol(StateMachine stateMachine, Enemy enemy, WaypointManager waypointManager) : base(stateMachine, enemy)
	{
		m_WaypointManager = m_Enemy.GetComponent<WaypointManager>();
		m_NavMeshAgent = m_Enemy.GetComponent<NavMeshAgent>();
	}

	public override void OnEnter()
	{
		if (!GetNextWaypoint())
		{
			m_StateMachine.RequestStateChange(m_Enemy.States.StateIdle);
			return;
		}
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate()
	{
		//Get Distance to Current Waypoint
		float distance = Vector3.Distance(m_Enemy.transform.position, m_CurrentWaypoint.Position);

		//If we aree within range of the waypoint...
		if (distance <= m_Enemy.Settings.DistanceToWaypointSatisfaction)
		{
			//...Check that we have satisfied the Wait Time
			if (m_WaitTime <= 0)
			{
				//...Get the next waypoint
				if (!GetNextWaypoint())
				{
					m_StateMachine.RequestStateChange(m_Enemy.States.StateIdle);
					return;
				}
			}
			else
			{
				//...Reduce the Wait Time
				m_WaitTime -= Time.deltaTime;
			}
		}

		//Set Enemy's walking animation
		m_Enemy.SetWalkAnimation(!Mathf.Approximately(m_NavMeshAgent.velocity.magnitude, 0));
	}

	private bool GetNextWaypoint()
	{
		if (m_WaypointManager.GetNextWaypoint(out m_CurrentWaypoint))
		{
			m_WaitTime = m_CurrentWaypoint.WaitTime;
			m_NavMeshAgent.SetDestination(m_CurrentWaypoint.Position);
			return true;
		}

		return false;
	}
}
