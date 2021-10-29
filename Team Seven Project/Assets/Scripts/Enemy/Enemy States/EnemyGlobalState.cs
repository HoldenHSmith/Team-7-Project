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
		
	}


	public override bool ReceiveMessage(Telegram message)
	{
		switch (message.MessageType)
		{
			case MessageType.Msg_PlayerSpotted:
				//Set state to go to position
				Enemy.LastKnownPlayerPos = (Vector3)message.ExtraInfo;
				StateMachine.RequestStateChange(Enemy.EnemyStates.StateCameraDetectedPlayer);
				return true;

			case MessageType.Msg_Reset:
				StateMachine.RequestStateChange(Enemy.EnemyStates.StateIdle);
				return true;

			case MessageType.Msg_Sound:
				SoundEmission sound = (SoundEmission)message.ExtraInfo;
				Debug.Log($"Sound Heard... Volume: {sound.Volume}  |  Position: {sound.Position}  |  Distance Falloff: {sound.DistanceFalloff}");
				return true;

			default:
				return false;

		}
	}
}
