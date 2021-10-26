public abstract class CharacterState
{
	protected StateMachine StateMachine;

	public CharacterState(StateMachine stateMachine)
	{
		StateMachine = stateMachine;
	}

	public abstract void OnEnter();

	public abstract void OnUpdate();

	public abstract void OnExit();


}
