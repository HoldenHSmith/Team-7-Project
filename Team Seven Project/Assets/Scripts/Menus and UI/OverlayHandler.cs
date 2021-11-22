using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class OverlayHandler : MonoBehaviour
{

	[SerializeField] private Image _beakerIconOn = null;
	[SerializeField] private Image _beakerIconOff = null;

	//Ui Elements for Note Overlay
	[SerializeField] private Canvas _noteOverlay = null;
	[SerializeField] private TextMeshProUGUI _noteTitle = null;
	[SerializeField] private TextMeshProUGUI _noteBody = null;

	[SerializeField] private GameObject _keycardRed = null;
	[SerializeField] private GameObject _keycardGreen = null;
	[SerializeField] private GameObject _keycardBlue = null;
	[SerializeField] private GameObject _keycardMini = null;
	[SerializeField] private TextMeshProUGUI _keycardCount = null;
	[SerializeField] private Image _staminaBar = null;
	[SerializeField] private List<AudioClip> _noteAudioClips = new List<AudioClip>();
	[SerializeField] private AudioSource _audioSource = null;
	[SerializeField] private PauseMenuHandler _pauseMenuHandler = null;
	private PlayerCharacter _player = null;
	private bool _notePopup = false;

	private void Start()
	{
		_player = GameManager.Instance.Player;
		GameManager.Instance.CollectionManager.OverlayHandler = this;
		GameManager.Instance.OverlayHandler = this;
		_noteOverlay.enabled = false;
		_player.OverlayHandler = this;

	}

	private void Update()
	{
		if (_player != null)
		{
			if (_player.HasBeaker)
			{
				_beakerIconOn.gameObject.SetActive(true);
				_beakerIconOff.gameObject.SetActive(false);
			}
			else
			{
				_beakerIconOff.gameObject.SetActive(true);
				_beakerIconOn.gameObject.SetActive(false);
			}
		}
		if (_noteOverlay.enabled && (Keyboard.current.eKey.wasReleasedThisFrame || Keyboard.current.escapeKey.wasReleasedThisFrame) && !_notePopup)
		{
			_noteOverlay.enabled = false;
			Time.timeScale = 1;
			_pauseMenuHandler.CanToggle = true;
		}
		SetMiniKeycardCount(GameManager.Instance.Player.MiniKeycards);
		_notePopup = false;
	}

	public void ReadNote(PaperNote note)
	{
		_noteOverlay.enabled = true;
		_noteTitle.text = note.Title;
		_noteBody.text = note.Body;
		_notePopup = true;
		Time.timeScale = 0;
		PlayNoteSound();
		_pauseMenuHandler.CanToggle = false;
	}

	private void PlayNoteSound()
	{
		if (_noteAudioClips.Count <= 0)
			return;
		int index = Random.Range(0, _noteAudioClips.Count);
		_audioSource.PlayOneShot(_noteAudioClips[index]);

	}

	public void SetKeycardActive(AreaType area, bool active)
	{
		switch (area)
		{
			case AreaType.Containment:
				_keycardRed.SetActive(active);
				break;
			case AreaType.Biolab:
				_keycardGreen.SetActive(active);
				break;
			case AreaType.Surveillance:
				_keycardBlue.SetActive(active);
				break;
		}
	}

	public void SetMiniKeycardCount(int amount)
	{
		if (amount <= 0)
		{
			_keycardCount.gameObject.SetActive(false);
			_keycardMini.SetActive(false);
		}
		else
		{
			_keycardCount.gameObject.SetActive(true);
			_keycardMini.SetActive(true);
		}

		_keycardCount.text = "" + amount;


	}

	public void UpdateStaminaBar(float fillAmount)
	{
		_staminaBar.fillAmount = fillAmount;
	}



}
