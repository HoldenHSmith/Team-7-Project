
public class EnemyStatePlayerDetected : EnemyState
{
	public EnemyStatePlayerDetected(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{

	}

	public override void OnEnter()
	{
		throw new System.NotImplementedException();
	}

	public override void OnExit()
	{
		throw new System.NotImplementedException();
	}

	public override void OnUpdate(float deltaTime)
	{
		throw new System.NotImplementedException();
	}

	public override bool ReceiveMessage(Telegram message)
	{
		return false;
	}
}
