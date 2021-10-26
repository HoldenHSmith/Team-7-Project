//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Player Inputs")]

	[Tooltip("Checks if the player's Input is blocked")]
	[SerializeField] private bool _playerInputBlocked = false;

	protected Vector2 MovementInput;                      //Stores movement input values by the player
	protected bool ExternalInputBlocked;
	protected bool CrouchPressed;
	protected bool LeftMouseDown;
	protected float LeftMouseDownTime;
	protected bool InteractionKeyPressed;
	protected Mouse CurrentMouse;

	protected void SetupInput()
	{

		Input = new CharacterInput();

		//Subscribe movement inputs
		Input.Player.Movement.started += ctx => OnMovementInput(ctx);
		Input.Player.Movement.performed += ctx => OnMovementInput(ctx);
		Input.Player.Movement.canceled += ctx => OnMovementInput(ctx);

		//Subscribe crouch movements
		Input.Player.Crouch.started += ctx => OnCrouchInput(ctx);
		Input.Player.Crouch.performed += ctx => OnCrouchInput(ctx);
		Input.Player.Crouch.canceled += ctx => OnCrouchInput(ctx);

		//Subscribe to Left Mouse
		Input.Player.AimingThrowing.started += ctx => OnLeftMouse(ctx);
		Input.Player.AimingThrowing.performed += ctx => OnLeftMouse(ctx);
		Input.Player.AimingThrowing.canceled += ctx => OnLeftMouse(ctx);

		//Subscripe to interaction key
		Input.Player.Interaction.started += ctx => OnInteractionKey(ctx);
		Input.Player.Interaction.performed += ctx => OnInteractionKey(ctx);
		Input.Player.Interaction.canceled += ctx => OnInteractionKey(ctx);

		CurrentMouse = Mouse.current;
	}

	protected void UpdateInputs()
	{
		if (LeftMouseDown)
			LeftMouseDownTime += Time.deltaTime;
	}

	protected void EndInputUpdate()
	{
		if (CurrentMouse.leftButton.wasReleasedThisFrame)
			LeftMouseDownTime = 0;
	}

	//Update movement inputs
	protected void OnMovementInput(InputAction.CallbackContext context) => MovementInput = context.ReadValue<Vector2>();

	//Update crouch input
	protected void OnCrouchInput(InputAction.CallbackContext context) => CrouchPressed = context.ReadValueAsButton();

	//Reads in the Players Movement Input
	protected void OnInputMovement(InputAction.CallbackContext context) => MovementInput = context.ReadValue<Vector2>();

	//Reads the Left Mouse Button Input
	protected void OnLeftMouse(InputAction.CallbackContext context) => LeftMouseDown = context.ReadValueAsButton();

	//Reads true if interaction key is pressed
	protected void OnInteractionKey(InputAction.CallbackContext context) => InteractionKeyPressed = context.ReadValueAsButton();

	//Checks if movement input is pressed
	public bool IsMoveInput
	{
		get { return !Mathf.Approximately(MovementInput.sqrMagnitude, 0f); }
	}

	//Checks if the crouch input has been pressed
	public bool IsCrouchInput
	{
		get { return CrouchPressed; }
	}

	//Returns movement input vector. If movement is blocked, returns 0
	public Vector2 MoveInput
	{
		get
		{
			if (_playerInputBlocked || ExternalInputBlocked)
				return Vector2.zero;
			return MovementInput;
		}
	}

	private void OnEnable()
	{
		//Enables Player Input
		Input.Enable();
	}

	private void OnDisable()
	{
		//Disables Player Input
		Input.Disable();
	}

}
