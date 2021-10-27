public abstract class CharacterState : IMessageReceiver
{
	protected StateMachine StateMachine;

	public CharacterState(StateMachine stateMachine)
	{
		StateMachine = stateMachine;
	}

	public abstract void OnEnter();

	public abstract void OnUpdate(float deltaTime);

	public abstract void OnExit();

	public abstract bool ReceiveMessage(Telegram message);
}
