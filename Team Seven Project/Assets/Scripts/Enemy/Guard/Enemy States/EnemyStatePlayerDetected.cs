using UnityEngine;

public class EnemyStatePlayerDetected : EnemyState
{
	public EnemyStatePlayerDetected(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{

	}

	public override void OnEnter()
	{
		Enemy.AlertnessState.SetAlertLevel(EnemyAlertState.AlertLevel.FoundPlayer);
		Enemy.NavAgent.speed = Enemy.Settings.RunSpeed;
		Enemy.NavAgent.acceleration = Enemy.Settings.RunAcceleration;
		Enemy.NavAgent.angularSpeed = Enemy.Settings.RunTurnSpeed;
		GameManager.Instance.Alerted = true;
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate(float deltaTime)
	{
		Vector3 playerPos = Enemy.GameManager.Player.transform.position;
		float distanceToPlayer = Vector3.Distance(Enemy.transform.position, playerPos);

		if (distanceToPlayer <= Enemy.Settings.FollowPlayerDistance)
			Enemy.NavAgent.destination = playerPos;
		else
		{
			StateMachine.RequestStateChange(Enemy.EnemyStates.StateInvestigate);
			Enemy.LastKnownPlayerPos = playerPos;
		}
	}

	public override bool ReceiveMessage(Telegram message)
	{
		return false;
	}
}
