using UnityEngine;

public class EnemyAlertState : MonoBehaviour
{
	public enum AlertLevel
	{
		None,
		Investigating,
		FoundPlayer
	}

	[SerializeField] private AlertLevel _alertness;
	private EnemyMaterialBlockHandler _propertyBlock;

	private void Awake()
	{
		_propertyBlock = GetComponentInChildren<EnemyMaterialBlockHandler>();
	}

	private void OnAlertChanged()
	{
		float evaluation = 0.0f;
		float speed = 0.0f;
		switch (_alertness)
		{
			case AlertLevel.None:
				break;
			case AlertLevel.Investigating:
				evaluation = 0.35f;
				speed = 5.0f;
				break;
			case AlertLevel.FoundPlayer:
				evaluation = 1.0f;
				speed = 15.0f;
				break;
			default:
				break;
		}

		_propertyBlock.SetProperties(evaluation, speed);
	}

	public void SetAlertLevel(AlertLevel level)
	{
		_alertness = level;
		OnAlertChanged();
	}

	public EnemyMaterialBlockHandler PropertyBlock { get => _propertyBlock; }
}
