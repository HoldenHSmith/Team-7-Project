using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemySettings))]
[RequireComponent(typeof(WaypointManager))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAlertState))]
public class Enemy : MonoBehaviour
{
	public float DetectRange; //Move these to a radar type script
	public float DetectAngle; //Move these to a radar type script

	protected NavMeshAgent Agent;
	protected EnemyStates States;
	protected StateMachine StateMachine;
	protected EnemySettings EnemySettings;
	protected EnemyAlertState AlertState;

	//Move to separate class
	protected Animator animator;
	private int walkHash;

	private void Awake()
	{
		Agent = GetComponent<NavMeshAgent>();
		StateMachine = new StateMachine();
		States = new EnemyStates();
		States.OnStart(StateMachine, this);
		StateMachine.RequestStateChange(States.StatePatrol);
		EnemySettings = GetComponent<EnemySettings>();
		AlertState = GetComponent<EnemyAlertState>();
		//Test
		animator = GetComponentInChildren<Animator>();
		walkHash = Animator.StringToHash("Walking");
	}

	private void Update()
	{
		StateMachine.MyUpdate();
	}

	//Add this to it's own class
	public void SetWalkAnimation(bool value)
	{
		animator.SetBool(walkHash, value);

		//REMOVE THIS
		if(value)
		{
			AlertState.SetAlertLevel(EnemyAlertState.AlertLevel.Investigating);
		}
		else
			AlertState.SetAlertLevel(EnemyAlertState.AlertLevel.None);
	}

	public EnemyStates EnemyStates { get => States; }
	public StateMachine EnemyStateMachine { get => StateMachine; }
	public EnemySettings Settings { get => EnemySettings; }
}

