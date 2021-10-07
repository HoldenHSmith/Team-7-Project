
public class EnemyStates 
{
	public EnemyStateIdle StateIdle;
	public EnemyStateInvestigate StateInvestigate;
	public EnemyStatePatrol StatePatrol;
	public EnemyStatePlayerDetected StatePlayerDetected;

	public void OnStart(StateMachine stateMachine,Enemy enemy)
	{
		StateIdle = new EnemyStateIdle(stateMachine, enemy);
		StateInvestigate = new EnemyStateInvestigate(stateMachine, enemy);
		StatePatrol = new EnemyStatePatrol(stateMachine, enemy,enemy.GetComponent<WaypointManager>());
		StatePlayerDetected = new EnemyStatePlayerDetected(stateMachine, enemy);
	}

}
