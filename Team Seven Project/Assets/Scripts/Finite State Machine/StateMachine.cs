//Written by Jayden Hunter
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	protected Dictionary<string, CharacterState> m_StateDict;
	protected GameObject m_Owner;

	public GameObject Owner { get => m_Owner; }
	public Dictionary<string, CharacterState> States { get => m_StateDict; }

	protected CharacterState m_CurrentState;
	protected CharacterState m_PreviousState;

	public void RequestStateChange(CharacterState state)
	{
		if (state == null)
			return;

		//Exit Current State
		if (m_CurrentState != null)
			m_CurrentState.OnExit();

		//Set Previous state to Current State
		m_PreviousState = m_CurrentState;

		//Set the new Current State
		m_CurrentState = state;

		//Enter the Current State
		m_CurrentState.OnEnter();

	}

	//Updates the Finite State Machine and processes the Current State's logic
	public void MyUpdate()
	{
		if (m_CurrentState != null)
		{
			m_CurrentState.OnUpdate();
		}
	}

	public CharacterState CurrentState { get => m_CurrentState; }

}
