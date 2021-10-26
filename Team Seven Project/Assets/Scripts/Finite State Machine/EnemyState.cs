//Written by Jayden Hunter

public abstract class EnemyState  : CharacterState
{
	protected Enemy Enemy;

	public EnemyState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
	{
		Enemy = enemy;
	}

}
