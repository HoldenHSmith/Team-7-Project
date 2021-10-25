using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	protected int AnimCrouchHash;
	protected int AnimWalkHash;
	protected Animator Animator;

	protected void SetupAnimator()
	{
		Animator = GetComponentInChildren<Animator>();
		AnimCrouchHash = Animator.StringToHash("Crouching");
		AnimWalkHash = Animator.StringToHash("Walking");
	}

	protected void UpdateAnimations()
	{
		Animator.SetBool(AnimCrouchHash, IsCrouchInput);
		Animator.SetBool(AnimWalkHash, IsMoveInput);
	}
}
