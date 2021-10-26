public class EnemyStateIdle : EnemyState
{
	public EnemyStateIdle(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{
	}

	public override void OnEnter()
	{
		Enemy.SetWalkAnimation(false);
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate()
	{

	}

	public override bool ReceiveMessage(Telegram message)
	{
		return false;
	}
}
