using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class GameManager : MonoBehaviour, IMessageSender
{
	//Singleton
	private static GameManager _instance;
	private EnemyManager _enemyManager;
	private MessageDispatcher _messageDispatcher;
	private PlayerCharacter _playerCharacter;
	private CollectionManager _collectionManager;

	public void SendMessage()
	{

	}

	private void Awake()
	{
		_playerCharacter = GameObject.Find("Player").GetComponent<PlayerCharacter>();
		_enemyManager = GetComponentInChildren<EnemyManager>();
		_messageDispatcher = MessageDispatcher.Instance;
		_collectionManager = new CollectionManager();

		if (_instance == null)
			_instance = this;
		else
			DestroyImmediate(this);
	}

	private void Start()
	{
		List<Enemy> enemies = EnemyManager.Enemies;

		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].GameManager = this;
		}
	}

	private void Update()
	{
		if (Keyboard.current.rightBracketKey.wasPressedThisFrame)
		{
			SendMessage();
		}
	}

	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject go = new GameObject();
				_instance = go.AddComponent<GameManager>();
				go.name = "Game Manager";
			}
			return _instance;
		}
	}

	public PlayerCharacter Player { get => _playerCharacter; }
	public CollectionManager Collections { get => _collectionManager; }
}
