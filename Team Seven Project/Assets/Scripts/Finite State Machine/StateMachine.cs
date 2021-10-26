//Written by Jayden Hunter
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	public Dictionary<string, CharacterState> States { get => StateDictionary; }

	protected Dictionary<string, CharacterState> StateDictionary;
	protected GameObject Owner;
	protected CharacterState CurrentState;
	protected CharacterState PreviousState;

	public void RequestStateChange(CharacterState state)
	{
		if (state == null)
			return;

		//Exit Current State
		if (CurrentState != null)
			CurrentState.OnExit();

		//Set Previous state to Current State
		PreviousState = CurrentState;

		//Set the new Current State
		CurrentState = state;

		//Enter the Current State
		CurrentState.OnEnter();

	}

	//Updates the Finite State Machine and processes the Current State's logic
	public void MyUpdate()
	{
		if (CurrentState != null)
		{
			CurrentState.OnUpdate();
		}
	}

	public CharacterState StateCurrent { get => StateCurrent; }

}
