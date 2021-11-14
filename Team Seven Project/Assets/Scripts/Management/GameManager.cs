using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CollectionManager))]
[RequireComponent(typeof(NoteManager))]
[RequireComponent(typeof(KeycardManager))]
[RequireComponent(typeof(DoorManager))]
public sealed class GameManager : MonoBehaviour, IMessageSender
{
	//Singleton
	private static GameManager _instance;
	private MessageDispatcher _messageDispatcher;
	private PlayerCharacter _playerCharacter;

	private SaveData _saveData;

	// Managers.
	private SaveManager _saveManager;
	private EnemyManager _enemyManager;
	private CollectionManager _collectionManager;
	private NoteManager _noteManager;
	private KeycardManager _keycardManager;
	private DoorManager _doorManager;


	public void SendMessage()
	{

	}

	private void Awake()
	{

		if (_instance == null)
			_instance = this;
		else
			DestroyImmediate(this);

		_playerCharacter = GameObject.Find("Player").GetComponent<PlayerCharacter>();
		_messageDispatcher = MessageDispatcher.Instance;

		//Get Managers
		_saveManager = SaveManager.Instance;
		_enemyManager = GetComponentInChildren<EnemyManager>();
		_collectionManager = GetComponent<CollectionManager>();
		_noteManager = GetComponent<NoteManager>();
		_keycardManager = GetComponent<KeycardManager>();
		_doorManager = GetComponent<DoorManager>();

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
			_doorManager.SetLockedStatuses(s.DoorStatusesToList());
			_doorManager.SetMiniLockedStatues(s.MiniKeycardDoorsUnlocked);
			_keycardManager.LoadKeycards(s.KeyDict());
			_keycardManager.LoadMinikeycards(s.MiniKeycardsCollected);
			//Load note stuff
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
			SaveManager.Save(Player.transform.position, Player.transform.rotation);
		}
		if (Keyboard.current.leftBracketKey.wasPressedThisFrame)
		{
			SaveManager.ClearSave();
		}

		if (Keyboard.current.escapeKey.wasReleasedThisFrame)
		{
			Application.Quit();
		}
	}

	public static GameManager Instance
	{
		get
		{
			return _instance;
		}
	}



	public PlayerCharacter Player { get => _playerCharacter; }
	public CollectionManager Collections { get => _collectionManager; }
	public CollectionManager CollectionManager { get => _collectionManager; }
	public NoteManager NoteManager { get => _noteManager; set => _noteManager = value; }
	public KeycardManager KeycardManager { get => _keycardManager; set => _keycardManager = value; }
	public DoorManager DoorManager { get => _doorManager; set => _doorManager = value; }
}
