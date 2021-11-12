using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
    private int _animCrouchHash;
    private int _animWalkHash;
    private Animator _animator;

    protected void SetupAnimator()
    {
        _animator = GetComponentInChildren<Animator>();
        _animCrouchHash = Animator.StringToHash("Crouching");
        _animWalkHash = Animator.StringToHash("Walking");
    }

    protected void UpdateAnimations()
    {
        _animator.SetBool(_animCrouchHash, IsSprintInput && Stamina > 0);
        _animator.SetBool(_animWalkHash, IsMoveInput);
    }
}
