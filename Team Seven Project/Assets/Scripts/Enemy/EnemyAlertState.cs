using UnityEngine;

public class EnemyAlertState : MonoBehaviour
{
	public enum AlertLevel
	{
		None,
		Investigating,
		FoundPlayer
	}

	[SerializeField] private AlertLevel m_Alertness;
	private EnemyMaterialBlockHandler m_propertyBlock;

	private void Awake()
	{
		m_propertyBlock = GetComponentInChildren<EnemyMaterialBlockHandler>();
	}

	private void OnAlertChanged()
	{
		float evaluation = 0.0f;
		float speed = 0.0f;
		switch (m_Alertness)
		{
			case AlertLevel.None:
				break;
			case AlertLevel.Investigating:
				//evaluation = 0.35f;
				//speed = 5.0f;
				break;
			case AlertLevel.FoundPlayer:
				//evaluation = 1.0f;
				//speed = 15.0f;
				break;
			default:
				break;
		}

		m_propertyBlock.SetProperties(evaluation, speed);
	}

	public void SetAlertLevel(AlertLevel level)
	{
		m_Alertness = level;
		OnAlertChanged();
	}
}
