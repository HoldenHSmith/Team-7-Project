//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Tooltip("Checks if the player's Input is blocked")]
	public bool playerInputBlocked;

	protected Vector2 m_MovementInput;                      //Stores movement input values by the player

	protected bool m_ExternalInputBlocked;
	protected bool m_CrouchPressed;

	protected void SetupInput()
	{
		m_Input = new CharacterInput();

		//Subscribe movement inputs
		m_Input.Player.Movement.started += ctx => OnMovementInput(ctx);
		m_Input.Player.Movement.performed += ctx => OnMovementInput(ctx);
		m_Input.Player.Movement.canceled += ctx => OnMovementInput(ctx);

		//Subscribe crouch movements
		m_Input.Player.Crouch.started += ctx => OnCrouchInput(ctx);
		m_Input.Player.Crouch.performed += ctx => OnCrouchInput(ctx);
		m_Input.Player.Crouch.canceled += ctx => OnCrouchInput(ctx);
	}

	//Update movement inputs
	protected void OnMovementInput(InputAction.CallbackContext context)
	{
		m_MovementInput = context.ReadValue<Vector2>();
	}

	//Update crouch input
	protected void OnCrouchInput(InputAction.CallbackContext context)
	{
		m_CrouchPressed = context.ReadValueAsButton();
	}

	//Reads in the Players Movement Input
	public void OnInputMovement(InputAction.CallbackContext ctx) => m_MovementInput = ctx.ReadValue<Vector2>();

	//Checks if movement input is pressed
	public bool IsMoveInput
	{
		get { return !Mathf.Approximately(m_MovementInput.sqrMagnitude, 0f); }
	}

	//Checks if the crouch input has been pressed
	public bool IsCrouchInput
	{
		get { return m_CrouchPressed; }
	}

	//Returns movement input vector. If movement is blocked, returns 0
	public Vector2 MoveInput
	{
		get
		{
			if (playerInputBlocked || m_ExternalInputBlocked)
				return Vector2.zero;
			return m_MovementInput;
		}
	}

	private void OnEnable()
	{
		//Enables Player Input
		m_Input.Enable();
	}

	private void OnDisable()
	{
		//Disables Player Input
		m_Input.Disable();
	}

}
