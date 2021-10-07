//Written by Jayden Hunter

public abstract class EnemyState  : CharacterState
{
	protected Enemy m_Enemy;

	public EnemyState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
	{
		m_Enemy = enemy;
	}


	
}
