using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
    [Tooltip("The speed in which the character rotates")]
    [SerializeField] private float _rotationSpeed = 10.0f;

    protected void UpdateRotation()
    {
        if (IsMoveInput)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-_movementInput.x, 0, -MoveInput.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        if (_leftMouseDown && !IsMoveInput && HasBeaker)
        {
            Vector3 directionToThrow = transform.position - _landingZoneSprite.transform.position;
            directionToThrow.y = 0;
            directionToThrow.Normalize();
            Quaternion lookRotation = Quaternion.LookRotation(directionToThrow);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

            //_movementblocked = true;
        }
        //else _movementblocked = false;
    }
}
