//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space,Header("Movement Mechanics")]
	[Tooltip("The maximum move speed that the character can move")]
	[SerializeField] private float m_MaxVelocity;           //How fast the character can move
	[Tooltip("The maximum move speed that the character can move while crouching")]
	[SerializeField] private float m_MaxCrouchVelocity;           //How fast the character can move while crouching
	[Tooltip("Time in seconds it takes the character to reach max speed")]
	[SerializeField] private float m_TimeToMaxSpeed;        //Time in seconds to reach max speed
	[Tooltip("Time in seconds it takes the character to stop")]
	[SerializeField] private float m_TimeToZero;            //Time in seconds to stop

	protected float m_Acceleration; //Acceleration rate of the character
	protected float m_Deceleration; //Acceleration rate of the character
	protected Vector3 m_Velocity; //Character's current velocity

	protected void SetupMovement()
	{
		m_Acceleration = m_MaxVelocity / m_TimeToMaxSpeed;
		m_Deceleration = -m_MaxVelocity / m_TimeToZero;
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
			targetVelocity = direction * m_MaxVelocity;
		else
			targetVelocity = direction * m_MaxCrouchVelocity;

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
			if (m_Velocity.magnitude > m_MaxVelocity)
			{
				m_Velocity = m_Velocity.normalized * m_MaxVelocity;
			}
		}
		else
		{
			if(m_Velocity.magnitude > m_MaxCrouchVelocity)
			{
				m_Velocity = m_Velocity.normalized * m_MaxCrouchVelocity;
			}
		}

	}

	public void MoveCharacter(Vector3 motion)
	{
		m_CharacterController.Move(motion);
	}

	

}
