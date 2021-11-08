//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space,Header("Movement Mechanics")]

	[Tooltip("The maximum move speed that the character can move")]
	[SerializeField] private float _maxVelocity = 0;           //How fast the character can move

	[Tooltip("The maximum move speed that the character can move while crouching")]
	[SerializeField] private float _maxCrouchVelocity = 0;           //How fast the character can move while crouching

	[Tooltip("Time in seconds it takes the character to reach max speed")]
	[SerializeField] private float _timeToMaxSpeed = 0;        //Time in seconds to reach max speed

	[Tooltip("Time in seconds it takes the character to stop")]
	[SerializeField] private float _timeToZero = 0;            //Time in seconds to stop

	private float _acceleration; //Acceleration rate of the character
	private float _deceleration; //Acceleration rate of the character
	private Vector3 _velocity; //Character's current velocity
	
	protected void SetupMovement()
	{
		_acceleration = _maxVelocity / _timeToMaxSpeed;
		_deceleration = -_maxVelocity / _timeToZero;
		_velocity = Vector3.zero;
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
		if (!CrouchPressed)
			targetVelocity = direction * _maxVelocity;
		else
			targetVelocity = direction * _maxCrouchVelocity;

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
		if (!CrouchPressed)
		{
			if (_velocity.magnitude > _maxVelocity)
			{
				_velocity = _velocity.normalized * _maxVelocity;
			}
		}
		else
		{
			if(_velocity.magnitude > _maxCrouchVelocity)
			{
				_velocity = _velocity.normalized * _maxCrouchVelocity;
			}
		}

	}

	public void MoveCharacter(Vector3 motion)
	{
		CharacterController.SimpleMove(motion);
	}

	

}
