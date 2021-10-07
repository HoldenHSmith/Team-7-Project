//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Tooltip("The maximum move speed that the character can move")]
	public float MaxVelocity;           //How fast the character can move
	[Tooltip("The maximum move speed that the character can move while crouching")]
	public float MaxCrouchVelocity;           //How fast the character can move while crouching
	[Tooltip("Time in seconds it takes the character to reach max speed")]
	public float TimeToMaxSpeed;        //Time in seconds to reach max speed
	[Tooltip("Time in seconds it takes the character to stop")]
	public float TimeToZero;            //Time in seconds to stop

	protected float m_Acceleration; //Acceleration rate of the character
	protected float m_Deceleration; //Acceleration rate of the character
	protected Vector3 m_Velocity; //Character's current velocity

	protected void SetupMovement()
	{
		m_Acceleration = MaxVelocity / TimeToMaxSpeed;
		m_Deceleration = -MaxVelocity / TimeToZero;
		m_Velocity = Vector3.zero;
	}

	//Updates the character's velocity based on player input
	private void UpdateVelocity()
	{

		//Only increase the velocity if the character's current speed is less than the maximum speed
		Vector2 moveInputNormalized = MoveInput.normalized;

		Vector3 direction = new Vector3(moveInputNormalized.x, 0, moveInputNormalized.y);
		Vector3 targetVelocity = Vector3.zero;

		//Change this to use state machine for player
		if (!m_CrouchPressed)
			targetVelocity = direction * MaxVelocity;
		else
			targetVelocity = direction * MaxCrouchVelocity;

		Vector3 difference = targetVelocity - m_Velocity;

		if (targetVelocity.magnitude == 0)
		{
			m_Velocity -= difference * m_Deceleration * Time.fixedDeltaTime;
		}
		else
		{
			m_Velocity += difference * m_Acceleration * Time.fixedDeltaTime;
		}

		//Limit the character's velocity
		if (!m_CrouchPressed)
		{
			if (m_Velocity.magnitude > MaxVelocity)
			{
				m_Velocity = m_Velocity.normalized * MaxVelocity;
			}
		}
		else
		{
			if(m_Velocity.magnitude > MaxCrouchVelocity)
			{
				m_Velocity = m_Velocity.normalized * MaxCrouchVelocity;
			}
		}

	}

	public void MoveCharacter(Vector3 motion)
	{
		m_CharacterController.Move(motion);
	}

	

}
