public class EnemyStateIdle : EnemyState
{
	public EnemyStateIdle(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{
	}

	public override void OnEnter()
	{
		Enemy.AnimationHandler.SetWalk(0,EnemyWalkSpeed.idle);
		Enemy.AlertnessState.SetAlertLevel(EnemyAlertState.AlertLevel.None);
		Enemy.NavAgent.speed = Enemy.Settings.WalkSpeed;
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate(float deltaTime)
	{

	}

	public override bool ReceiveMessage(Telegram message)
	{
		return false;
	}
}
