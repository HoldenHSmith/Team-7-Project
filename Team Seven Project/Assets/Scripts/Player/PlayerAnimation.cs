using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	protected int m_AnimCrouchHash;
	protected int m_AnimWalkHash;
	protected Animator m_Animator;

	protected void SetupAnimator()
	{
		m_Animator = GetComponentInChildren<Animator>();
		m_AnimCrouchHash = Animator.StringToHash("Crouching");
		m_AnimWalkHash = Animator.StringToHash("Walking");
	}

	protected void UpdateAnimations()
	{
		m_Animator.SetBool(m_AnimCrouchHash, IsCrouchInput);
		m_Animator.SetBool(m_AnimWalkHash, IsMoveInput);
	}
}
