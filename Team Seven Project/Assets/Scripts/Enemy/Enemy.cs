using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(EnemySettings))]
[RequireComponent(typeof(WaypointManager))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAlertState))]
public class Enemy : MonoBehaviour, IMessageReceiver
{
	public float DetectRange; //Move these to a radar type script
	public float DetectAngle; //Move these to a radar type script

	protected NavMeshAgent Agent;
	protected EnemyStates States;
	protected StateMachine StateMachine;
	protected EnemySettings EnemySettings;
	protected EnemyAlertState AlertState;

	private Vector3 _lastKnownPlayerPosition = Vector3.zero;

	//Move to separate class
	protected Animator animator;
	private int walkHash;
	
	private void Awake()
	{
		Agent = GetComponent<NavMeshAgent>();
		StateMachine = new StateMachine();
		States = new EnemyStates();
		StateMachine.SetGlobalState(new EnemyGlobalState(StateMachine, this));
		//StateMachine.
		States.OnStart(StateMachine, this);
		
		EnemySettings = GetComponent<EnemySettings>();
		AlertState = GetComponent<EnemyAlertState>();
		//Test
		animator = GetComponentInChildren<Animator>();
		walkHash = Animator.StringToHash("Walking");
	}

	private void Start()
	{
		StateMachine.RequestStateChange(States.StatePatrol);
	}
	
	private void Update()
	{
		StateMachine.MyUpdate();
	}

	//Add this to it's own class
	public void SetWalkAnimation(bool value)
	{
		animator.SetBool(walkHash, value);

	}

	public void OnEnable()
	{
		EnemyManager.RegisterEnemy(this);
	}

	public void OnDisable()
	{
		EnemyManager.RemoveEnemy(this);
	}

	public bool ReceiveMessage(Telegram message)
	{
		return StateMachine.ReceiveMessage(message);
	}


	public EnemyStates EnemyStates { get => States; }
	public StateMachine EnemyStateMachine { get => StateMachine; }
	public EnemySettings Settings { get => EnemySettings; }
	public EnemyAlertState AlertnessState { get => AlertState; }
	public NavMeshAgent NavAgent { get => Agent; }
	public Vector3 LastKnownPlayerPos { get => _lastKnownPlayerPosition; set => _lastKnownPlayerPosition = value; }
}

