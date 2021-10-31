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

	}

	private void Awake()
	{
		_playerCharacter = GameObject.Find("Player").GetComponent<PlayerCharacter>();
		_enemyManager = GetComponentInChildren<EnemyManager>();
		_messageDispatcher = MessageDispatcher.Instance;
	}

	private void Start()
	{
		List<Enemy> enemies = EnemyManager.Enemies;

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
