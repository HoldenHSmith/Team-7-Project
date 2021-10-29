using UnityEngine;

public class AudioDetection : MonoBehaviour, IMessageSender
{
	[SerializeField] private float _detectionRange = 10.0f;
	[SerializeField] private float _alertnessReductionPerSecond = 2.5f;
	[SerializeField] private float _startReducingCooldown = 1.0f;

	private float _alertness = 0;
	private float _reductionCooldownTimer;

	public void SendMessage()
	{
		
	}

	private void Update()
	{
		if (_alertness > 0)
			_reductionCooldownTimer -= Time.deltaTime;
	}

	private void OnDrawGizmosSelected()
	{
		DebugEx.DrawCircle(transform.position, _detectionRange, Color.yellow);
	}
}
