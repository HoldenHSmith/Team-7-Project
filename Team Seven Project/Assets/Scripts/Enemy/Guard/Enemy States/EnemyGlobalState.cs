using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		UpdateAlertness();
		CheckPlayerInRange();
	}

	private void CheckPlayerInRange()
	{
		float distanceToPlayer = Vector3.Distance(Enemy.transform.position, GameManager.Instance.Player.transform.position);

		if (distanceToPlayer <= Enemy.Settings.AutoDetectRange)
		{
			if (RaycheckPlayer())
				StateMachine.RequestStateChange(Enemy.EnemyStates.StatePlayerDetected);
		}

		if (distanceToPlayer <= Enemy.Settings.AutoCatchRange)
		{
			if (RaycheckPlayer())
			{
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			}
		}
	}

	private bool RaycheckPlayer()
	{
		RaycastHit hit;
		Vector3 directionToPlayer = GameManager.Instance.Player.transform.position - Enemy.transform.position;
		if (Physics.Raycast(Enemy.transform.position + Vector3.up, directionToPlayer, out hit))
		{
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				return true;
			}
		}
		return false;
	}

	private void UpdateAlertness()
	{
		float awareness = Enemy.AudioDetector.Alertness;

		if (StateMachine.StateCurrent == Enemy.EnemyStates.StatePatrol)
		{
			float flashSpeed = (awareness / 100) * 5;
			if (flashSpeed >= 4)
				flashSpeed = 4;
			Enemy.AlertnessState.PropertyBlock.SetProperties(0, flashSpeed);
		}
	}

	private void UpdateAnimations()
	{
		//Set Enemy's walking animation
		Enemy.AnimationHandler.SetWalk(Enemy.NavAgent.velocity.magnitude, Enemy.WalkState);
	}

	public override bool ReceiveMessage(Telegram message)
	{
		switch (message.MessageType)
		{
			case MessageType.Msg_PlayerSpottedByCamera:
				if (StateMachine.StateCurrent != Enemy.EnemyStates.StatePlayerDetected)
				{
					Enemy.LastKnownPlayerPos = (Vector3)message.ExtraInfo;
					StateMachine.RequestStateChange(Enemy.EnemyStates.StateCameraDetectedPlayer);
					return true;
				}
				return false;

			case MessageType.Msg_PlayerSpottedByGuard:
				StateMachine.RequestStateChange(Enemy.EnemyStates.StatePlayerDetected);
				return true;

			case MessageType.Msg_Reset:
				StateMachine.RequestStateChange(Enemy.EnemyStates.StateIdle);
				return true;

			case MessageType.Msg_Sound:
				if (StateMachine.StateCurrent != Enemy.EnemyStates.StatePlayerDetected)
				{
					SoundEmission sound = (SoundEmission)message.ExtraInfo;
					Enemy.AudioDetector.ProcessSound(sound);
					if (Enemy.AudioDetector.ThresholdReached())
					{
						Enemy.LastKnownPlayerPos = sound.Position;
						StateMachine.RequestStateChange(Enemy.EnemyStates.StateInvestigate);
						return true;
					}
				}
				return false;

			default:
				return false;

		}
	}
}
