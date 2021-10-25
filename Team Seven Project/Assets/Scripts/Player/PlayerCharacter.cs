//Written by Jayden Hunter
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public partial class PlayerCharacter : MonoBehaviour
{
	public bool IsRespawning { get { return Respawning; } }
	
	protected CharacterInput Input;                       //Reference to Input System
	protected bool Respawning;                            //Whether the character is respawning
	protected CharacterController CharacterController;    //Characters controller

	private void Awake()
	{
		GetRequiredComponents();
		SetupInput();
		SetupMovement();
		SetupAnimator();
		SetupThrow();
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		
		UpdateInputs();
		UpdateRotation();
		UpdateAnimations();
		UpdateVelocity();
		UpdateThrow();
		EndInputUpdate();
		MoveCharacter(Velocity);
	}

	private void FixedUpdate()
	{
		
	}

	//Reset is called automatically by Unity when the script is first added to a gameobject or is reset 
	private void Reset()
	{
		GetRequiredComponents();
	}

	//Get the required components of the character controller
	private void GetRequiredComponents()
	{
		CharacterController = GetComponent<CharacterController>();
	}

}
