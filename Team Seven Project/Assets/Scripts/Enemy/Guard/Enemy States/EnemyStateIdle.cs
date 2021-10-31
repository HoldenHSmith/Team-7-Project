public class EnemyStateIdle : EnemyState
{
	public EnemyStateIdle(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{
	}

	public override void OnEnter()
	{
		Enemy.WalkState = EnemyWalkSpeed.idle;
		Enemy.AlertnessState.SetAlertLevel(EnemyAlertState.AlertLevel.None);
		Enemy.NavAgent.speed = Enemy.Settings.WalkSpeed;
		Enemy.NavAgent.acceleration = Enemy.Settings.WalkAcceleration;
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
