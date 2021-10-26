using UnityEngine;

public class GameManager : MonoBehaviour
{
	private EnemyManager _enemyManager;

	private void Awake()
	{
		_enemyManager = GetComponentInChildren<EnemyManager>();
	}
}
