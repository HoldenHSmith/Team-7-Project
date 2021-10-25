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

	protected NavMeshAgent m_Agent;
	protected EnemyStates m_States;
	protected StateMachine m_StateMachine;
	protected EnemySettings m_EnemySettings;
	protected EnemyAlertState m_AlertState;

	//Move to separate class
	protected Animator animator;
	private int walkHash;

	private void Awake()
	{
		m_Agent = GetComponent<NavMeshAgent>();
		m_StateMachine = new StateMachine();
		m_States = new EnemyStates();
		m_States.OnStart(m_StateMachine, this);
		m_StateMachine.RequestStateChange(m_States.StatePatrol);
		m_EnemySettings = GetComponent<EnemySettings>();
		m_AlertState = GetComponent<EnemyAlertState>();
		//Test
		animator = GetComponentInChildren<Animator>();
		walkHash = Animator.StringToHash("Walking");
	}

	private void Update()
	{
		m_StateMachine.MyUpdate();
	}

	//Add this to it's own class
	public void SetWalkAnimation(bool value)
	{
		animator.SetBool(walkHash, value);

		//REMOVE THIS
		if(value)
		{
			m_AlertState.SetAlertLevel(EnemyAlertState.AlertLevel.Investigating);
		}
		else
			m_AlertState.SetAlertLevel(EnemyAlertState.AlertLevel.None);
	}

	public EnemyStates States { get => m_States; }
	public StateMachine StateMachine { get => m_StateMachine; }
	public EnemySettings Settings { get => m_EnemySettings; }
}

