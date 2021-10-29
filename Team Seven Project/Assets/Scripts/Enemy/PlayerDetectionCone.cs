﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RecipientHandler))]
public class PlayerDetectionCone : MonoBehaviour, IMessageSender
{
	[Tooltip("The transform from which the cone and light will originate from.")]
	[SerializeField] private Transform _coneDetectionTransform;

	[SerializeField] private float _viewConeAngle = 25;
	[SerializeField] private float _distance = 25;
	[SerializeField] private bool _followPlayer = true;
	[SerializeField] private float _maxFollowDistance = 5.0f;
	[SerializeField] private float _messageDelay = 0.25f;
	[SerializeField] private float _lightSoftness = 0.0f;
	[SerializeField] private Color _lightColor = Color.red;
	[SerializeField] private float _lightIntensity = 25.0f;
	[SerializeField] private Light _spotLight;

	private GameManager _gameManager;
	private PlayerCharacter _player;
	private RecipientHandler _recipientHandler;
	private float _messageDelayTimer;

	//Debugging
	[SerializeField] private bool _debugCone = true;
	[SerializeField] private Color _debugColor = Color.red;



	private void Start()
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		if (_gameManager == null)
			Debug.LogError("GameManager prefab was not found in the scene.");

		_player = _gameManager.Player;

		if (_player == null)
			Debug.LogError("Player prefab was not found in the scene.");

		if (_coneDetectionTransform == null)
			Debug.LogError("Cone detection transform not assigned.");
		//_simpleRotate = GetComponent<SimpleRotate>();
		_recipientHandler = GetComponent<RecipientHandler>();
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

	private void FollowPlayer()
	{
		//float distanceToPlayer = Vector3.Distance(_coneDetectionTransform.position, _player.transform.position);
		//if (distanceToPlayer < _maxFollowDistance)
		//{
		//	//transform.LookAt(_player.transform.position);
		//	//if (_simpleRotate != null)
		//	//	_simpleRotate.enabled = false;
		//	//_spotLight.range = distanceToPlayer * 2;
		//}
		//else
		//{
		//	_playerSeen = false;
		//	//if (_simpleRotate != null)
		//	//	_simpleRotate.enabled = true;

		//}
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
				Debug.Log("Camera Spotted Player!");
				if (Vector3.Distance(_coneDetectionTransform.position, samplePoints[i].position) < _distance)
				{
					//Do raycast
					//_playerSeen = true;
					return true;
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
		_spotLight.range = _distance;
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
				MessageDispatcher.Instance.DispatchMessage(0, this, recipients[i], MessageType.Msg_PlayerSpotted, _player.transform.position);
			}
		}
	}
}
