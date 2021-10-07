public abstract class CharacterState
{
	protected StateMachine m_StateMachine;

	public CharacterState(StateMachine stateMachine)
	{
		m_StateMachine = stateMachine;
	}

	public abstract void OnEnter();

	public abstract void OnUpdate();

	public abstract void OnExit();


}
