using UnityEngine;

public class EnemyAnimator
{
	private Animator _animator;
	private EnemySettings _settings;

	private float _walkMapFromValue;
	private float _walkMapToValue = 0.5f;
	private float _walkInspectMapFromValue;
	private float _walkInspectMapToValue = 0.5f;
	private float _runMapFromValue;
	private float _runMapToValue = 1.0f;

	private int _movementSpeedHash;
	private int _idleMotionHash;

	public EnemyAnimator(EnemySettings settings, Animator animator)
	{
		_settings = settings;
		_animator = animator;

		CalculateRemapvalues();

		_movementSpeedHash = Animator.StringToHash("Movement Speed");
		_idleMotionHash = Animator.StringToHash("Idle");
	}

	public void CalculateRemapvalues()
	{
		_walkMapFromValue = _settings.WalkSpeed;
		_walkInspectMapFromValue = _settings.WalkInspectSpeed;
		_runMapFromValue = _settings.RunSpeed;

	}

	public void PlayAnimationOnce(string name)
	{
		_animator.Play(name);
	}

	public void SetWalk(float movementSpeed, EnemyWalkSpeed walkState)
	{
		switch (walkState)
		{
			case EnemyWalkSpeed.idle:
				_animator.SetFloat(_movementSpeedHash, 0);
				break;
			case EnemyWalkSpeed.normal:
				_animator.SetFloat(_movementSpeedHash, MathJ.RemapValues(movementSpeed, 0, _walkMapFromValue, 0, _walkMapToValue));
				break;
			case EnemyWalkSpeed.investigate:
				_animator.SetFloat(_movementSpeedHash, MathJ.RemapValues(movementSpeed, 0, _walkInspectMapFromValue, 0, _walkInspectMapToValue));
				break;
			case EnemyWalkSpeed.run:
				_animator.SetFloat(_movementSpeedHash, MathJ.RemapValues(movementSpeed, 0, _runMapFromValue, 0, _runMapToValue));
				break;
			default:
				break;
		}
	}
}
