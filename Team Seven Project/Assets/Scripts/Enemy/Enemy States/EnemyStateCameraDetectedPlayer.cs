using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateCameraDetectedPlayer : EnemyState
{
	public EnemyStateCameraDetectedPlayer(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{

	}

	public override void OnEnter()
	{
		Enemy.AlertnessState.SetAlertLevel(EnemyAlertState.AlertLevel.FoundPlayer);
		Enemy.NavAgent.speed = Enemy.Settings.WalkCameraAlertSpeed;
		Enemy.NavAgent.destination = Enemy.LastKnownPlayerPos;
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate(float deltaTime)
	{
		if (Vector3.Distance(Enemy.transform.position, Enemy.LastKnownPlayerPos) < Enemy.Settings.DistanceToLastKnownPlayerPosition)
			StateMachine.RequestStateChange(Enemy.EnemyStates.StateInvestigate);
	}

	public override bool ReceiveMessage(Telegram message)
	{
		return false;
	}
}
