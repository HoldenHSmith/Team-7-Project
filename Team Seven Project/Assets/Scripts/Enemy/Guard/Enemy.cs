using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(EnemySettings))]
[RequireComponent(typeof(WaypointManager))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAlertState))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(AudioDetection))]
public class Enemy : MonoBehaviour, IMessageReceiver
{
	public float DetectRange; //Move these to a radar type script
	public float DetectAngle; //Move these to a radar type script

	protected NavMeshAgent Agent;
	protected EnemyStates States;
	protected StateMachine StateMachine;
	protected EnemySettings EnemySettings;
	protected EnemyAlertState AlertState;
	protected EnemyAnimator Animator;
	protected AudioDetection AudioDetection;

	private Vector3 _lastKnownPlayerPosition = Vector3.zero;
	private GameManager _gameManager;
	private EnemyWalkSpeed _walkState;

	private void Awake()
	{
		Agent = GetComponent<NavMeshAgent>();

		StateMachine = new StateMachine();
		States = new EnemyStates();
		StateMachine.SetGlobalState(new EnemyGlobalState(StateMachine, this));
		States.OnStart(StateMachine, this);

		EnemySettings = GetComponent<EnemySettings>();
		AlertState = GetComponent<EnemyAlertState>();
		Animator = new EnemyAnimator(EnemySettings, GetComponentInChildren<Animator>());
		AudioDetection = GetComponent<AudioDetection>();
	}

	private void OnDrawGizmos()
	{
		//if (Application.isPlaying)
		//	DebugEx.DrawViewArch(transform.position, transform.rotation, Settings.ViewConeAngle, 10, Color.red);
	}

	private void Start()
	{
		StateMachine.RequestStateChange(States.StatePatrol);
	}

	private void Update()
	{
		StateMachine.MyUpdate();
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

	private void OnValidate()
	{
		if (Application.isPlaying && Application.isEditor && Animator != null)
			Animator.CalculateRemapvalues();
	}

	public EnemyStates EnemyStates { get => States; }
	public StateMachine EnemyStateMachine { get => StateMachine; }
	public EnemySettings Settings { get => EnemySettings; }
	public EnemyAlertState AlertnessState { get => AlertState; }
	public NavMeshAgent NavAgent { get => Agent; }
	public Vector3 LastKnownPlayerPos { get => _lastKnownPlayerPosition; set => _lastKnownPlayerPosition = value; }
	public GameManager GameManager { get => _gameManager; set => _gameManager = value; }
	public EnemyAnimator AnimationHandler { get => Animator; }
	public EnemyWalkSpeed WalkState { get => _walkState; set => _walkState = value; }
	public AudioDetection AudioDetector { get => AudioDetection; }

}

public enum EnemyWalkSpeed
{
	idle,
	normal,
	investigate,
	run
}

