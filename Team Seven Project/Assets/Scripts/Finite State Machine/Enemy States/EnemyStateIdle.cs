public class EnemyStateIdle : EnemyState
{
	public EnemyStateIdle(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{
	}

	public override void OnEnter()
	{
		m_Enemy.SetWalkAnimation(false);
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate()
	{

	}
}
