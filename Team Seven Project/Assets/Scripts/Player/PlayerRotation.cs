using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Tooltip("The speed in which the character rotates")]
	[SerializeField] private float _rotationSpeed;

	protected void UpdateRotation()
	{
		if(IsMoveInput)
		{
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-MovementInput.x, 0, -MoveInput.y));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
		}
	}
}
