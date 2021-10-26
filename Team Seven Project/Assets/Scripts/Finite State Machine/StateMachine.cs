//Written by Jayden Hunter
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : IMessageReceiver
{
	public Dictionary<string, CharacterState> States { get => StateDictionary; }

	protected Dictionary<string, CharacterState> StateDictionary;
	protected GameObject Owner;
	protected CharacterState CurrentState;
	protected CharacterState PreviousState;
	protected CharacterState GlobalState;

	public void RequestStateChange(CharacterState state)
	{
		if (state == null)
			return;

		// Exit Current State.
		if (CurrentState != null)
			CurrentState.OnExit();

		// Set Previous state to Current State.
		PreviousState = CurrentState;

		// Set the new Current State.
		CurrentState = state;

		// Enter the Current State.
		CurrentState.OnEnter();

	}

	// Updates the Finite State Machine and processes the Current State's logic.
	public void MyUpdate()
	{
		if (CurrentState != null)
		{
			CurrentState.OnUpdate();
		}

		if(GlobalState != null)
		{
			GlobalState.OnUpdate();
		}
	}

	public bool ReceiveMessage(Telegram message)
	{
		// Check that the current state is valid and can handle the message.
		if (CurrentState != null && CurrentState.ReceiveMessage(message))
		{
			return true;
		}
		if(GlobalState != null && GlobalState.ReceiveMessage(message))
		{
			return true;
		}
		return false;
	}

	public void SetGlobalState(CharacterState state)
	{
		GlobalState = state;
	}

	public CharacterState StateCurrent { get => StateCurrent; }

}
