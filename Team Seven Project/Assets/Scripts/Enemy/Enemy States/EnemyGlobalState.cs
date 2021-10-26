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

	public override void OnUpdate()
	{
		
	}

	public override bool ReceiveMessage(Telegram message)
	{
		switch (message.MessageType)
		{
			case MessageType.Msg_PlayerSpotted:
				//Set state to go to position
				Debug.Log("Player Was Spotted! (Called by Enemy Global State");
				return true;
	
			case MessageType.Msg_Reset:
				StateMachine.RequestStateChange(Enemy.EnemyStates.StateIdle);
				return true;

			default:
				return false;

		}
	}
}
