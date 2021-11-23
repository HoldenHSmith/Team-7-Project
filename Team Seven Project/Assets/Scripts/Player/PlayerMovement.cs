//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Movement Mechanics")]

	[Tooltip("The maximum move speed that the character can move.")]
	[SerializeField] private float _maxVelocity = 0;           //How fast the character can move

	[Tooltip("The maximum move speed that the character can move while crouching.")]
	[SerializeField] private float _maxSprintVelocity = 0;           //How fast the character can move while crouching

	[Tooltip("Time in seconds it takes the character to reach max speed.")]
	[SerializeField] private float _timeToMaxSpeed = 0;        //Time in seconds to reach max speed

	[Tooltip("Time in seconds it takes the character to stop.")]
	[SerializeField] private float _timeToZero = 0;            //Time in seconds to stop

	[Tooltip("The amount of stamina the player has.")]
	[SerializeField] private float _maxStamina = 100;

	[Tooltip("The amount of seconds it takes to fully recover stamina.")]
	[SerializeField] private float _staminaRecoveryTime = 5;

	[Tooltip("The amount of seconds the player can sprint for.")]
	[SerializeField] private float _maxSprintTime = 2;

	private float _acceleration; //Acceleration rate of the character
	private float _deceleration; //Acceleration rate of the character
	private Vector3 _velocity; //Character's current velocity

	[SerializeField] protected float Stamina; //Character's current stamina
	private float _staminaRecoveryRate; //Characters stamina recovery rate based on Recovery Time
	private float _staminaDepletionRate; //Characters stamina depletion rate based on max sprint time;
	private OverlayHandler _overlayHander;

	protected void SetupMovement()
	{
		Stamina = _maxStamina;
		_acceleration = _maxVelocity / _timeToMaxSpeed;
		_deceleration = -_maxVelocity / _timeToZero;
		_velocity = Vector3.zero;

		_staminaRecoveryRate = _maxStamina / _staminaRecoveryTime;
		_staminaDepletionRate = _maxStamina / _maxSprintTime;

	}

	//Updates the character's velocity based on player input
	private void UpdateVelocity()
	{

		//Only increase the velocity if the character's current speed is less than the maximum speed
		Vector2 moveInputNormalized = MoveInput.normalized;

		//Vector3 direction = new Vector3(moveInputNormalized.x * , 0, moveInputNormalized.y);
		Vector3 direction = Vector3.zero;
		direction = Vector3.right * (moveInputNormalized.x);
		direction += Vector3.forward * (moveInputNormalized.y);
		direction.Normalize();

		Vector3 targetVelocity = Vector3.zero;

		//Change this to use state machine for player
		if (!_sprintPressed || Stamina <= 0)
			targetVelocity = direction * _maxVelocity;
		else
			targetVelocity = direction * _maxSprintVelocity;

		Vector3 difference = targetVelocity - _velocity;

		if (targetVelocity.magnitude == 0)
		{
			_velocity -= (difference * _deceleration) * Time.deltaTime;
		}
		else
		{
			_velocity += (difference * _acceleration) * Time.deltaTime;
		}

		//Limit the character's velocity
		if (!_sprintPressed)
		{
			if (_velocity.magnitude > _maxVelocity)
			{
				_velocity = _velocity.normalized * _maxVelocity;
			}
			//Recover Stamina
			if (Stamina < _maxStamina)
			{
				Stamina += _staminaRecoveryRate * Time.deltaTime;
				if (Stamina > _maxStamina)
					Stamina = _maxStamina;

			}
		}
		else if (Stamina > 0 && _sprintPressed && IsMoveInput)
		{
			Stamina -= _staminaDepletionRate * Time.deltaTime;
			if (_velocity.magnitude > _maxSprintVelocity)
			{
				_velocity = _velocity.normalized * _maxSprintVelocity;
			}
		}

		if (Stamina < 0)
			Stamina = 0;

		if (_overlayHander != null)
			_overlayHander.UpdateStaminaBar(Stamina / _maxStamina);

	}

	public void MoveCharacter(Vector3 motion)
	{
		_characterController.SimpleMove(motion);
	}

	public OverlayHandler OverlayHandler { get => _overlayHander; set => _overlayHander = value; }


}
