//Written by Jayden Hunter
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public partial class PlayerCharacter : MonoBehaviour
{
	public ushort playerID { get { return m_PlayerID; } }
	public bool respawning { get { return m_Respawning; } }
	
	protected ushort m_PlayerID;                            //Current ID of the player
	protected CharacterInput m_Input;                       //Reference to Input System
	protected bool m_Respawning;                            //Whether the character is respawning
	protected CharacterController m_CharacterController;    //Characters controller

	private void Awake()
	{
		GetRequiredComponents();
		SetupInput();
		SetupMovement();
		SetupAnimator();
		SetupThrow();
	}

	private void Update()
	{
		UpdateInputs();
		UpdateRotation();
		UpdateAnimations();
		UpdateThrow();
		EndInputUpdate();
	}

	private void FixedUpdate()
	{
		UpdateVelocity();
		MoveCharacter(m_Velocity);
	}

	//Reset is called automatically by Unity when the script is first added to a gameobject or is reset 
	private void Reset()
	{
		GetRequiredComponents();
	}

	//Get the required components of the character controller
	private void GetRequiredComponents()
	{
		m_CharacterController = GetComponent<CharacterController>();
	}

}
