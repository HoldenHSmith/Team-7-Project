//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Player Inputs")]

	[Tooltip("Checks if the player's Input is blocked")]
	[SerializeField] private bool m_PlayerInputBlocked;

	protected Vector2 m_MovementInput;                      //Stores movement input values by the player

	protected bool m_ExternalInputBlocked;
	protected bool m_CrouchPressed;
	protected bool m_LeftMouseDown;
	protected float m_LeftMouseDownTime;

	protected Mouse m_CurrentMouse;

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

		m_Input.Player.AimingThrowing.started += ctx => OnLeftMouse(ctx);
		m_Input.Player.AimingThrowing.performed += ctx => OnLeftMouse(ctx);
		m_Input.Player.AimingThrowing.canceled += ctx => OnLeftMouse(ctx);
		m_CurrentMouse = Mouse.current;
	}

	protected void UpdateInputs()
	{
		if (m_LeftMouseDown)
			m_LeftMouseDownTime += Time.deltaTime;
	}

	protected void EndInputUpdate()
	{
		if (m_CurrentMouse.leftButton.wasReleasedThisFrame)
			m_LeftMouseDownTime = 0;
	}

	//Update movement inputs
	protected void OnMovementInput(InputAction.CallbackContext context) => m_MovementInput = context.ReadValue<Vector2>();

	//Update crouch input
	protected void OnCrouchInput(InputAction.CallbackContext context) => m_CrouchPressed = context.ReadValueAsButton();

	//Reads in the Players Movement Input
	protected void OnInputMovement(InputAction.CallbackContext context) => m_MovementInput = context.ReadValue<Vector2>();

	//Reads the Left Mouse Button Input
	protected void OnLeftMouse(InputAction.CallbackContext context) => m_LeftMouseDown = context.ReadValueAsButton();

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
			if (m_PlayerInputBlocked || m_ExternalInputBlocked)
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
