using System.Collections.Generic;
using UnityEngine;

public class AudioDetection : MonoBehaviour, IMessageSender
{
	[SerializeField] private float _detectionRange = 10.0f;
	[SerializeField] private float _alertnessReductionPerSecond = 2.5f;
	[SerializeField] private float _startReducingCooldown = 1.0f;
	[SerializeField] private float _alertnessThreshold = 100;

	private float _alertness = 0;
	private float _reductionCooldownTimer;
	private LayerMask _ignoreLayer;

	public void SendMessage()
	{

	}

	private void Awake()
	{
		_ignoreLayer = ~(LayerMask.NameToLayer("Player"));
	}

	private void Update()
	{
		if (_alertness > 0)
			_reductionCooldownTimer -= Time.deltaTime;

		if (_reductionCooldownTimer <= 0 && _alertness > 0)
		{
			_alertness -= _alertnessReductionPerSecond * Time.deltaTime;
		}

		if (_alertness < 0)
			_alertness = 0;
		if (_alertness > _alertnessThreshold)
			_alertness = _alertnessThreshold;
	}

	public bool ThresholdReached()
	{
		if (_alertness >= _alertnessThreshold)
			return true;
		else
			return false;
	}

	public bool ProcessSound(SoundEmission sound)
	{
		float distance = Vector3.Distance(transform.position, sound.Position) - 1;

		if (distance <= _detectionRange)
		{
			Vector3 direction = sound.Position - transform.position;

			if (!Physics.Raycast(transform.position + Vector3.up, direction, distance, _ignoreLayer, QueryTriggerInteraction.Ignore))
			{
				Debug.Log($"{gameObject.name} Heard Player!");
				_alertness += sound.Volume;
				_reductionCooldownTimer = _startReducingCooldown;
				return true;

			}

			return false;
		}

		return false;
	}

	private void OnDrawGizmosSelected()
	{
		DebugEx.DrawCircle(transform.position, _detectionRange, Color.yellow);
	}

	public float Alertness { get => _alertness; }
}
