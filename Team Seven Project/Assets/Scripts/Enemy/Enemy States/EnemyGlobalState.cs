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
		if (PlayerDetected())
		{

		}

		//DebugEx.DrawViewArch(Enemy.transform.position, Enemy.transform.rotation, Enemy.Settings.ViewConeAngle, 10, Color.red);
	}


	private bool PlayerDetected()
	{
		// Get the direction from the enemy to the player and normalize it.
		Vector3 directionToPlayer = Enemy.GameManager.Player.transform.position - Enemy.transform.position;
		directionToPlayer.Normalize();

		//Conver the cone's field of view into the same unit type that is returned by a  dot product.
		float coneValue = Mathf.Cos((Enemy.Settings.ViewConeAngle * Mathf.Deg2Rad) * 0.5f);

		//Check if target is inside the cone
		if (Vector3.Dot(directionToPlayer, Enemy.transform.forward) >= coneValue)
		{
			Debug.Log("Player Detected!");
			//Player is inside the cone
			return true;
		}

		return false;
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

			default:
				return false;

		}
	}
}
