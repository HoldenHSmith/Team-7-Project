//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Player Inputs")]

	[Tooltip("Checks if the player's Input is blocked")]
	[SerializeField] private bool _playerInputBlocked = false;
	[SerializeField] private float _doorInteractBlockTime = 1.0f;
	[SerializeField] private float _keycardInteractBlockTime = 1.0f;

	private Vector2 _movementInput;                      //Stores movement input values by the player
	private bool _externalInputBlocked = false;
	private bool _sprintPressed;
	private bool _leftMouseDown;
	private float _leftMouseDownTime;
	private bool _interactionKeyPressed;
	private Mouse _currentMouse;
	private bool _movementblocked = false;
	protected bool _interactKeyReleasedThisFrame;
	private bool _rightMouseDown;
	private PauseMenuHandler _pauseMenu;
	private float _inputBlockTime;

	protected void SetupInput()
	{
		//pauseMenu = GameObject.Find("Pause Menu").GetComponent<PauseMenuHandler>();
		GameObject go = GameObject.Find("Pause Menu");
		if (go != null)
			go.TryGetComponent(out _pauseMenu);

		_input = new CharacterInput();

		//Subscribe movement inputs
		_input.Player.Movement.started += ctx => OnMovementInput(ctx);
		_input.Player.Movement.performed += ctx => OnMovementInput(ctx);
		_input.Player.Movement.canceled += ctx => OnMovementInput(ctx);

		//Subscribe crouch movements
		_input.Player.Crouch.started += ctx => OnSprintInput(ctx);
		_input.Player.Crouch.performed += ctx => OnSprintInput(ctx);
		_input.Player.Crouch.canceled += ctx => OnSprintInput(ctx);

		//Subscribe to Left Mouse
		_input.Player.AimingThrowing.started += ctx => OnLeftMouse(ctx);
		_input.Player.AimingThrowing.performed += ctx => OnLeftMouse(ctx);
		_input.Player.AimingThrowing.canceled += ctx => OnLeftMouse(ctx);

		//Subscribe to Right Mouse
		_input.Player.CancelThrow.started += ctx => OnRightMouse(ctx);
		_input.Player.CancelThrow.performed += ctx => OnRightMouse(ctx);
		_input.Player.CancelThrow.canceled += ctx => OnRightMouse(ctx);

		//Subscripe to interaction key
		_input.Player.Interaction.started += ctx => OnInteractionKey(ctx);
		_input.Player.Interaction.performed += ctx => OnInteractionKey(ctx);
		_input.Player.Interaction.canceled += ctx => OnInteractionKey(ctx);

		_input.Player.Interaction.canceled += ctx => OnInteractionReleased(ctx);

		_input.Player.Pause.started += ctx => OnPausePressed(ctx);

		_currentMouse = Mouse.current;
	}

	protected void UpdateInputs()
	{
		if (_leftMouseDown)
			_leftMouseDownTime += Time.deltaTime;

		if (_inputBlockTime > 0)
		{
			_inputBlockTime -= Time.deltaTime;
			_playerInputBlocked = true;
		}
		else
			_playerInputBlocked = false;
	}

	protected void EndInputUpdate()
	{
		if (_currentMouse.leftButton.wasReleasedThisFrame)
			_leftMouseDownTime = 0;
	}

	protected void ResetInputs()
	{
		_interactKeyReleasedThisFrame = false;
	}

	protected void BlockInputForTime(float duration)
	{
		_inputBlockTime = duration;
	}

	//Update movement inputs
	protected void OnMovementInput(InputAction.CallbackContext context) => _movementInput = (_playerInputBlocked || _externalInputBlocked || _movementblocked) ? Vector2.zero : context.ReadValue<Vector2>();

	//Update crouch input
	protected void OnSprintInput(InputAction.CallbackContext context) => _sprintPressed = (_playerInputBlocked || _externalInputBlocked) ? false : context.ReadValueAsButton();

	//Reads in the Players Movement Input
	protected void OnInputMovement(InputAction.CallbackContext context) => _movementInput = (_playerInputBlocked || _externalInputBlocked) ? Vector2.zero : context.ReadValue<Vector2>();

	//Reads the Left Mouse Button Input
	protected void OnLeftMouse(InputAction.CallbackContext context) => _leftMouseDown = (_playerInputBlocked || _externalInputBlocked) ? false : context.ReadValueAsButton();

	//Reads the Right Mouse Button Input
	protected void OnRightMouse(InputAction.CallbackContext context) => _rightMouseDown = (_playerInputBlocked || _externalInputBlocked) ? false : context.ReadValueAsButton();

	//Reads true if interaction key is pressed
	protected void OnInteractionKey(InputAction.CallbackContext context) => _interactionKeyPressed = (_playerInputBlocked || _externalInputBlocked) ? false : context.ReadValueAsButton();

	protected void OnInteractionReleased(InputAction.CallbackContext context) => _interactKeyReleasedThisFrame = !context.ReadValueAsButton();

	protected void OnPausePressed(InputAction.CallbackContext context)
	{
		// if (_pauseMenu != null)
		//  _pauseMenu.TogglePauseMenu();
	}

	//Checks if movement input is pressed
	public bool IsMoveInput
	{
		get
		{
			if (_playerInputBlocked || _externalInputBlocked)
				return false;

			return !Mathf.Approximately(_movementInput.sqrMagnitude, 0f);
		}
	}

	//Checks if the crouch input has been pressed
	public bool IsSprintInput
	{
		get { return _sprintPressed; }
	}

	//Returns movement input vector. If movement is blocked, returns 0
	public Vector2 MoveInput
	{
		get
		{
			if (_playerInputBlocked || _externalInputBlocked)
				return Vector2.zero;
			return _movementInput;
		}
	}

	private void OnEnable()
	{
		//Enables Player Input
		_input.Enable();
	}

	private void OnDisable()
	{
		//Disables Player Input
		_input.Disable();
	}

}
