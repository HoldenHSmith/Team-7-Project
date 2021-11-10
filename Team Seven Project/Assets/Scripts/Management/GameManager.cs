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
	private SaveManager _saveManager;

	private SaveData _saveData;

	public void SendMessage()
	{

	}

	private void Awake()
	{
		_playerCharacter = GameObject.Find("Player").GetComponent<PlayerCharacter>();
		_enemyManager = GetComponentInChildren<EnemyManager>();
		_messageDispatcher = MessageDispatcher.Instance;
		_saveManager = SaveManager.Instance;
		_collectionManager = new CollectionManager();

		if (_instance == null)
			_instance = this;
		else
			DestroyImmediate(this);


	}

	private void Start()
	{
		if (SaveManager.Load())
		{
			_saveData = _saveManager.Current;
		}
		if (_saveData != null)
		{
			SaveData s = _saveManager.Current;
			DoorManager.SetLockedStatuses(s.DoorStatusesToList());
			//_playerCharacter.transform.position = s.PosToVec3();
			KeycardManager.LoadKeycards(s.KeyDict());
			Debug.Log($"Game managers position read: {s.GetPosition()}");
		}
		else
			Debug.Log("No Save Data Found");

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
			SaveManager.Save(Player.transform.position,Player.transform.rotation);
		}
		if (Keyboard.current.leftBracketKey.wasPressedThisFrame)
		{
			SaveManager.ClearSave();
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
