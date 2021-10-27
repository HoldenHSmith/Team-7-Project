using UnityEngine;

public class PlayerDetectionCone : MonoBehaviour
{
	[SerializeField] private float _viewConeAngle;
	[SerializeField] private float _distance;
	[SerializeField] private bool _debugCone;
	[SerializeField] private Color _debugColor;

	private GameManager _gameManager;
	private PlayerCharacter _player;

	private void Start()
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		if (_gameManager == null)
			Debug.LogError("GameManager prefab was not found in the scene.");

		_player = _gameManager.Player;

		if (_player == null)
			Debug.LogError("Player prefab was not found in the scene.");
	}

	private void OnDrawGizmosSelected()
	{
		if (_debugCone)
			DebugEx.DrawViewCone(transform.position, transform.rotation, transform.forward, _viewConeAngle, _distance, _debugColor);
	}

	public bool PlayerDetected()
	{
		// Get the direction from the enemy to the player and normalize it.
		Vector3 directionToPlayer = _player.transform.position - _player.transform.position;
		directionToPlayer.Normalize();

		//Conver the cone's field of view into the same unit type that is returned by a  dot product.
		float coneValue = Mathf.Cos((_viewConeAngle * Mathf.Deg2Rad) * 0.5f);

		//Check if target is inside the cone
		if (Vector3.Dot(directionToPlayer, transform.forward) >= coneValue)
		{
			if (Vector3.Distance(transform.position, _player.transform.position) < _distance)
			{
				//Do raycast
				return true; ;
			}
		}

		return false;
	}


}
