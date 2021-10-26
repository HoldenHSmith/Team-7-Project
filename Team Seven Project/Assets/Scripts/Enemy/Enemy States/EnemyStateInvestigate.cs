public class EnemyStateInvestigate : EnemyState
{
	public EnemyStateInvestigate(StateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
	{

	}

	private float _investigationTimer;

	public override void OnEnter()
	{
		Enemy.AlertnessState.SetAlertLevel(EnemyAlertState.AlertLevel.Investigating);
		Enemy.NavAgent.speed = Enemy.Settings.WalkInspectSpeed;
		Enemy.NavAgent.isStopped = true;

		_investigationTimer = Enemy.Settings.InvestigationTime;
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate(float deltaTime)
	{
		_investigationTimer -= deltaTime;

		if (_investigationTimer <= 0)
			StateMachine.RequestStateChange(Enemy.EnemyStates.StatePatrol);
	}

	public override bool ReceiveMessage(Telegram message)
	{
		return false;
	}
}
