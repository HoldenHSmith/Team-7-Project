//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space,Header("Movement Mechanics")]
	[Tooltip("The maximum move speed that the character can move")]
	[SerializeField] private float _maxVelocity;           //How fast the character can move
	[Tooltip("The maximum move speed that the character can move while crouching")]
	[SerializeField] private float _maxCrouchVelocity;           //How fast the character can move while crouching
	[Tooltip("Time in seconds it takes the character to reach max speed")]
	[SerializeField] private float _timeToMaxSpeed;        //Time in seconds to reach max speed
	[Tooltip("Time in seconds it takes the character to stop")]
	[SerializeField] private float _timeToZero;            //Time in seconds to stop

	protected float Acceleration; //Acceleration rate of the character
	protected float Deceleration; //Acceleration rate of the character
	protected Vector3 Velocity; //Character's current velocity
	
	protected void SetupMovement()
	{
		Acceleration = _maxVelocity / _timeToMaxSpeed;
		Deceleration = -_maxVelocity / _timeToZero;
		Velocity = Vector3.zero;
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

		Vector3 difference = targetVelocity - Velocity;

		if (targetVelocity.magnitude == 0)
		{
			Velocity -= (difference * Deceleration) * Time.deltaTime;
		}
		else
		{
			Velocity += (difference * Acceleration) * Time.deltaTime;
		}

		//Limit the character's velocity
		if (!CrouchPressed)
		{
			if (Velocity.magnitude > _maxVelocity)
			{
				Velocity = Velocity.normalized * _maxVelocity;
			}
		}
		else
		{
			if(Velocity.magnitude > _maxCrouchVelocity)
			{
				Velocity = Velocity.normalized * _maxCrouchVelocity;
			}
		}

	}

	public void MoveCharacter(Vector3 motion)
	{
		CharacterController.Move(motion * Time.deltaTime);
	}

	

}
