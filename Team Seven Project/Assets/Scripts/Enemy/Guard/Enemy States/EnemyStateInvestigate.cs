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
	private int _investigateState = 0;

	private Vector3 _investigateDirection;
	private Vector3 _investigateDirectionRight;

	private float _investigationStartDelay = 1.3f;
	private float _investigationStartDelayTimer = 0;
	private bool _reachedLocation = false;
	public override void OnEnter()
	{
		_reachedLocation = false;
		//ChooseNextDestination();
		_investigateState = 0;
		Enemy.AudioDetector.Alertness = 50;
		Enemy.AlertnessState.SetAlertLevel(EnemyAlertState.AlertLevel.Investigating);
		Enemy.NavAgent.speed = Enemy.Settings.WalkInspectSpeed;
		Enemy.NavAgent.isStopped = false;

		ResetValues();
		Enemy.NavAgent.enabled = false;
		Enemy.WalkState = EnemyWalkSpeed.investigate;
		Enemy.NavAgent.acceleration = Enemy.Settings.InspectAcceleration;
		Enemy.NavAgent.angularSpeed = Enemy.Settings.InspectTurnSpeed;
		//_investigateDirection = _investigatePosition - Enemy.transform.position;
		//_investigateDirection.Normalize();
		//_investigateDirectionRight = Vector3.Cross(_investigateDirection, Vector3.up.normalized);
		//_investigateDirectionRight.Normalize();
		//_investigationDelayTimer = Enemy.Settings.InvestigationDelay;
		_investigationStartDelayTimer = _investigationStartDelay;
		Enemy.AnimationHandler.PlayAnimationOnce("Reaction");
	}

	private void ResetValues()
	{
		_investigationTimer = Enemy.Settings.InvestigationTime;
		_investigatePosition = Enemy.LastKnownPlayerPos;
		Vector3 direction = _investigatePosition - Enemy.transform.position;
		direction.Normalize();
		_investigatePosition -= direction * Enemy.Settings.DistanceToLastKnownPlayerPosition;
		NavMesh.SamplePosition(_investigatePosition, out NavMeshHit hit, 1, 1);
		_investigatePosition = hit.position;
		Enemy.AudioDetector.Alertness = 50;
		_reachedLocation = false;
	}

	public override void OnExit()
	{
		Enemy.NavAgent.enabled = true;
	}

	public override void OnUpdate(float deltaTime)
	{

		if (_investigationStartDelayTimer <= 0)
		{
			Enemy.NavAgent.enabled = true;
			Enemy.NavAgent.destination = _investigatePosition;
			_investigationTimer -= deltaTime;

			if (_investigationTimer <= 0)
			{
				_investigationTimer = Enemy.Settings.InvestigationDelay;
				ChooseNextDestination();
				_investigateState++;
			}

			Enemy.AnimationHandler.SetWalk(Enemy.NavAgent.velocity.magnitude, EnemyWalkSpeed.investigate);
		}
		else
			_investigationStartDelayTimer -= Time.deltaTime;

		if (Vector3.Distance(Enemy.transform.position, _investigatePosition) <= 0)
		{
			_reachedLocation = true;
			Enemy.AnimationHandler.PlayAnimationOnce("Reaction Slow");
		}
	}
	public void HeardNewSound()
	{
		ResetValues();
	}

	private void ChooseNextDestination()
	{
		StateMachine.RequestStateChange(Enemy.EnemyStates.StatePatrol);
		//Check Left
		//if (_investigateState == 0)
		//{

		//	Vector3 pos = _investigatePosition + (_investigateDirectionRight * 2) + (_investigateDirection*2);
		//	Debug.Log($"Calulated Pos: {pos}");
		//	Enemy.NavAgent.SetDestination(SampledPosition(pos));
		//	Debug.DrawLine(Enemy.transform.position, pos, Color.green, 2);
		//}
		////Check Right
		//else if (_investigateState == 1)
		//{

		//	Vector3 pos = _investigatePosition - (_investigateDirectionRight * 2) + (_investigateDirection * 2);
		//	Debug.Log($"Calulated Pos: {pos}");
		//	Enemy.NavAgent.SetDestination(SampledPosition(pos));
		//	Debug.DrawLine(Enemy.transform.position, pos, Color.green, 2);
		//}
		////Go back to patrolling
		//else
		//{
		//	StateMachine.RequestStateChange(Enemy.EnemyStates.StatePatrol);
		//}
		//Vector3 randomDirection = Random.insideUnitSphere * Enemy.Settings.InvestigationRadius;

		//randomDirection += Enemy.LastKnownPlayerPos;

		//NavMeshHit hit;
		//NavMesh.SamplePosition(randomDirection, out hit, Enemy.Settings.InvestigationRadius, 1);
		//_investigatePosition = hit.position;
		//_investigationDelayTimer = Enemy.Settings.InvestigationDelay;
		//Enemy.NavAgent.SetDestination(_investigatePosition);
	}

	private Vector3 SampledPosition(Vector3 v3Pos)
	{
		Vector3 sampledPos = Enemy.transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(v3Pos, out hit, Enemy.Settings.InvestigationRadius + 1, 1);
		sampledPos = hit.position;
		return sampledPos;
	}

	public override bool ReceiveMessage(Telegram message)
	{
		return false;
	}
}
