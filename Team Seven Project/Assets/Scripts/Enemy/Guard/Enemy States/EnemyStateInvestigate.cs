using UnityEngine;
using UnityEngine.AI;
public class EnemyStateInvestigate : EnemyState
{
	public EnemyStateInvestigate(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{

	}

	private float _investigationTimer;
	private float _investigationDelayTimer;
	private Vector3 _investigatePosition;

	public override void OnEnter()
	{
		//ChooseNextDestination();

		Enemy.AlertnessState.SetAlertLevel(EnemyAlertState.AlertLevel.Investigating);
		Enemy.NavAgent.speed = Enemy.Settings.WalkInspectSpeed;
		Enemy.NavAgent.isStopped = false;

		_investigationTimer = Enemy.Settings.InvestigationTime;
		_investigatePosition = Enemy.LastKnownPlayerPos;
		Enemy.NavAgent.destination = _investigatePosition;
		Enemy.WalkState = EnemyWalkSpeed.investigate;
		Enemy.NavAgent.acceleration = Enemy.Settings.InspectAcceleration;
		Enemy.NavAgent.angularSpeed = Enemy.Settings.InspectTurnSpeed;
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate(float deltaTime)
	{
		_investigationTimer -= deltaTime;


		if (_investigationTimer <= 0)
		{
			StateMachine.RequestStateChange(Enemy.EnemyStates.StatePatrol);
			return;
		}

		if (Vector3.Distance(Enemy.transform.position, Enemy.NavAgent.destination) <= Enemy.Settings.DistanceToWaypointSatisfaction)
		{
			_investigationDelayTimer -= deltaTime;

			if (_investigationDelayTimer <= 0)
				ChooseNextDestination();
		}

		Enemy.AnimationHandler.SetWalk(Enemy.NavAgent.velocity.magnitude, EnemyWalkSpeed.investigate);
	}

	private void ChooseNextDestination()
	{
		Vector3 randomDirection = Random.insideUnitSphere * Enemy.Settings.InvestigationRadius;

		randomDirection += Enemy.LastKnownPlayerPos;

		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, Enemy.Settings.InvestigationRadius, 1);
		_investigatePosition = hit.position;
		_investigationDelayTimer = Enemy.Settings.InvestigationDelay;
		Enemy.NavAgent.SetDestination(_investigatePosition);
	}

	public override bool ReceiveMessage(Telegram message)
	{
		return false;
	}
}
