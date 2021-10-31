using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlobalState : EnemyState
{
	public EnemyGlobalState(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{

	}


	public override void OnEnter()
	{

	}

	public override void OnExit()
	{

	}

	public override void OnUpdate(float deltaTime)
	{
		UpdateAnimations();
	}

	private void UpdateAnimations()
	{
		//Set Enemy's walking animation
		//if (StateMachine.StateCurrent == Enemy.EnemyStates.StatePatrol)
		//	Enemy.AnimationHandler.SetWalk(Enemy.NavAgent.velocity.magnitude, EnemyWalkSpeed.normal);
		//else if(StateMachine.StateCurrent == Enemy.EnemyStates.StateInvestigate)
		//	Enemy.AnimationHandler.SetWalk(Enemy.NavAgent.velocity.magnitude, EnemyWalkSpeed.investigate);
		Enemy.AnimationHandler.SetWalk(Enemy.NavAgent.velocity.magnitude, Enemy.WalkState);
	}

	public override bool ReceiveMessage(Telegram message)
	{
		switch (message.MessageType)
		{
			case MessageType.Msg_PlayerSpottedByCamera:
				Enemy.LastKnownPlayerPos = (Vector3)message.ExtraInfo;
				StateMachine.RequestStateChange(Enemy.EnemyStates.StateCameraDetectedPlayer);
				return true;

			case MessageType.Msg_PlayerSpottedByGuard:
				StateMachine.RequestStateChange(Enemy.EnemyStates.StatePlayerDetected);
				return true;

			case MessageType.Msg_Reset:
				StateMachine.RequestStateChange(Enemy.EnemyStates.StateIdle);
				return true;

			case MessageType.Msg_Sound:
				SoundEmission sound = (SoundEmission)message.ExtraInfo;
				Enemy.LastKnownPlayerPos = sound.Position;
				StateMachine.RequestStateChange(Enemy.EnemyStates.StateInvestigate);
				return true;

			default:
				return false;

		}
	}
}
