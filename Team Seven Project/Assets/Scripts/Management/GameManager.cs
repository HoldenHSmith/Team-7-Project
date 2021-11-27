using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CollectionManager))]
[RequireComponent(typeof(NoteManager))]
[RequireComponent(typeof(KeycardManager))]
[RequireComponent(typeof(DoorManager))]
public sealed class GameManager : MonoBehaviour, IMessageSender
{
	[SerializeField] private Volume _volume = null;
	[SerializeField] private AudioClip _siren = null;
	[SerializeField] private AudioSource _audioSource = null;
	[SerializeField] private Gradient _alarmGradient;

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
	private OverlayHandler _overlayHandler;
	private ColorAdjustments _colorAdjustments;
	private float _time;

	private bool _alerted = false;
	public void SendMessage()
	{

	}

	private void Awake()
	{

		_volume.profile.TryGet<ColorAdjustments>(out _colorAdjustments);


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

		_collectionManager.InitializeMinikeys(_keycardManager);
		_audioSource = GetComponent<AudioSource>();
		_audioSource.enabled = false;

	}

	private void Start()
	{
		_overlayHandler = _collectionManager.OverlayHandler;

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

			_overlayHandler.SetKeycardActive(AreaType.Containment, s.KeyDict()[AreaType.Containment]);
			_overlayHandler.SetKeycardActive(AreaType.Biolab, s.KeyDict()[AreaType.Biolab]);
			_overlayHandler.SetKeycardActive(AreaType.Surveillance, s.KeyDict()[AreaType.Surveillance]);

			_overlayHandler.SetMiniKeycardCount(Player.MiniKeycards);
		}
		else
		{
			_overlayHandler.SetKeycardActive(AreaType.Containment, false);
			_overlayHandler.SetKeycardActive(AreaType.Biolab, false);
			_overlayHandler.SetKeycardActive(AreaType.Surveillance, false);
			_overlayHandler.SetMiniKeycardCount(Player.MiniKeycards);
			Debug.Log("No Save Data Found");
		}



		List<Enemy> enemies = EnemyManager.Enemies;

		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].GameManager = this;
		}
	}

	private void Update()
	{
		//if (Keyboard.current.rightBracketKey.wasPressedThisFrame)
		//{
		//	SaveManager.Save(Player.transform.position, Player.transform.rotation);
		//}
		if (Keyboard.current.leftBracketKey.wasPressedThisFrame)
		{
			SaveManager.ClearSave();
		}

		//if (Keyboard.current.escapeKey.wasReleasedThisFrame)
		//{
		//	Application.Quit();
		//}

		HandleAlarm();

	}

	private void HandleAlarm()
	{
		if (_alerted)
		{
			_audioSource.enabled = true;
			_audioSource.clip = _siren;
			_audioSource.loop = true;
			if (!_audioSource.isPlaying)
				_audioSource.Play();

			float t = Mathf.PingPong(_time, 1);
			_colorAdjustments.colorFilter.Override(_alarmGradient.Evaluate(t));
			Debug.Log(t);
			_time += Time.deltaTime * 2;
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
	public OverlayHandler OverlayHandler { get => _overlayHandler; set => _overlayHandler = value; }
	public bool Alerted { get => _alerted; set => _alerted = value; }
}
