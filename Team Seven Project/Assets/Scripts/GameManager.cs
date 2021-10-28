using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour, IMessageSender
{
	private EnemyManager _enemyManager;
	private MessageDispatcher _messageDispatcher;
	private PlayerCharacter _playerCharacter;

	public void SendMessage()
	{
		_messageDispatcher.DispatchMessage(0, this, _enemyManager.Enemies[0], MessageType.Msg_PlayerSpotted, _playerCharacter.transform.position);
		_messageDispatcher.DispatchMessage(0, this, _enemyManager.Enemies[1], MessageType.Msg_PlayerSpotted, _playerCharacter.transform.position);
	}

	private void Awake()
	{
		_playerCharacter = GameObject.Find("Player").GetComponent<PlayerCharacter>();
		_enemyManager = GetComponentInChildren<EnemyManager>();
		_messageDispatcher = MessageDispatcher.Instance;
	}

	private void Start()
	{
		List<Enemy> enemies = _enemyManager.Enemies;

		for(int i =0; i< enemies.Count;i++)
		{
			enemies[i].GameManager = this;
		}
	}

	private void Update()
	{
		if(Keyboard.current.rightBracketKey.wasPressedThisFrame)
		{
			SendMessage();
		}
	}

	public PlayerCharacter Player { get => _playerCharacter; }
}
