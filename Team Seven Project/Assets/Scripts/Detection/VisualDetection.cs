using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RecipientHandler))]
public class VisualDetection : MonoBehaviour, IMessageSender
{
	[Tooltip("The transform from which the cone and light will originate from.")]
	[SerializeField] private Transform _coneDetectionTransform = null;

	[SerializeField] private float _viewConeAngle = 25;
	[SerializeField] private float _distance = 25;
	[SerializeField] private float _messageDelay = 0.25f;
	[SerializeField] private float _lightSoftness = 0.0f;
	[SerializeField] private Color _lightColor = Color.red;
	[SerializeField] private float _lightIntensity = 25.0f;
	[SerializeField] private Light _spotLight = null;
	[SerializeField] private DetectorType _detectorType = DetectorType.Camera;
	[Tooltip("The distance in which the gaurd captures the player and the game ends.")]
	[SerializeField] private float _catchDistance = 0.25f;
	private GameManager _gameManager;
	private PlayerCharacter _player;
	private RecipientHandler _recipientHandler;
	private float _messageDelayTimer;

	//Debugging
	[SerializeField] private bool _debugCone = true;
	[SerializeField] private Color _debugColor = Color.red;

	private void Start()
	{
		_gameManager = GameManager.Instance;

		if (_gameManager == null)
			Debug.LogError("GameManager prefab was not found in the scene.");

		_player = _gameManager.Player;

		if (_player == null)
			Debug.LogError("Player prefab was not found in the scene.");

		if (_coneDetectionTransform == null)
			Debug.LogError("Cone detection transform not assigned.");

		_recipientHandler = GetComponent<RecipientHandler>();

		AdjustLight();

	}

	private void OnDrawGizmosSelected()
	{
		if (_debugCone && _coneDetectionTransform != null)
			DebugEx.DrawViewCone(_coneDetectionTransform.position, _coneDetectionTransform.rotation, _coneDetectionTransform.forward, _viewConeAngle * 0.5f, _distance, _debugColor);
	}

	private void Update()
	{

		if (PlayerDetected())
		{
			SendMessage();

		}
	}

	public bool PlayerDetected()
	{
		List<Transform> samplePoints = _player.SamplePoints;

		for (int i = 0; i < samplePoints.Count; i++)
		{
			// Get the direction from the enemy to the player and normalize it.
			Vector3 directionToPlayer = samplePoints[i].position - _coneDetectionTransform.position;
			directionToPlayer.Normalize();

			//Conver the cone's field of view into the same unit type that is returned by a  dot product.
			float coneValue = Mathf.Cos((_viewConeAngle * Mathf.Deg2Rad) * 0.5f);

			float playerDotProd = Vector3.Dot(directionToPlayer, _coneDetectionTransform.forward);

			//Check if target is inside the cone
			if (playerDotProd >= coneValue)
			{
				float distanceToPlayer = Vector3.Distance(_coneDetectionTransform.position, samplePoints[i].position);
				if (distanceToPlayer < _distance)
				{
					RaycastHit hit;
					Debug.DrawRay(_coneDetectionTransform.position, directionToPlayer, Color.cyan, 0.01f);
					if (Physics.Raycast((_coneDetectionTransform.position) + Vector3.up, directionToPlayer, out hit))
					{
						if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
						{
							Debug.Log($"{gameObject.name} Spotted Player!");
							if (distanceToPlayer <= _catchDistance && _detectorType == DetectorType.Guard)
							{
								Scene scene = SceneManager.GetActiveScene();
								SceneManager.LoadScene(scene.name);
							}
							return true;
						}
						Debug.Log($"{hit.collider.gameObject.name}");

					}
				}
			}
		}

		return false;
	}

	private void OnValidate()
	{
		AdjustLight();
	}

	private void AdjustLight()
	{
		if (_spotLight == null)
			return;

		if (_spotLight.type != LightType.Spot)
			_spotLight.type = LightType.Spot;

		_spotLight.innerSpotAngle = _viewConeAngle - _lightSoftness;
		_spotLight.spotAngle = _viewConeAngle;
		_spotLight.range = _distance * 1.1f;
		_spotLight.color = _lightColor;
		_spotLight.intensity = _lightIntensity;

	}

	public void SendMessage()
	{
		_messageDelayTimer -= Time.deltaTime;

		if (_messageDelayTimer <= 0)
		{
			Debug.Log("Message sent!");
			_messageDelayTimer = _messageDelay;
			List<IMessageReceiver> recipients = _recipientHandler.Recipients;

			for (int i = 0; i < recipients.Count; i++)
			{
				if (_detectorType == DetectorType.Guard)
					MessageDispatcher.Instance.DispatchMessage(0, this, recipients[i], MessageType.Msg_PlayerSpottedByGuard, _player.transform.position);
				else
					MessageDispatcher.Instance.DispatchMessage(0, this, recipients[i], MessageType.Msg_PlayerSpottedByCamera, _player.transform.position);
			}
		}
	}
}

public enum DetectorType
{
	Guard,
	Camera
}
