using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsHandler : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _title;
	[SerializeField] private TextMeshProUGUI _name;
	[SerializeField] private TextMeshProUGUI _exitText;
	[SerializeField] private AudioClip _scanClip = null;
	private AudioSource _audioSource;

	private int _currentName = 0;
	private int _subjectNumber = 1996;

	private string[] _titles =
	{
		"PRODUCER",
		"LEAD DESIGNER",
		"DESIGNER",
		"LEAD PROGRAMMER",
		"LEAD ARTIST",
		"ARTIST",
		"ARTIST",
		"ARTIST",
		"ARTIST",
		"SPECIAL THANKS",
		"SPECIAL THANKS",
		"SPECIAL THANKS",
		"SOUND SOURCE"
	};

	private string[] _names =
{
		"HOLDEN SMITH",
		"FLYNN VAN STRYP",
		"ZAC REICHELT",
		"JAYDEN HUNTER",
		"BEN LINDRIDGE",
		"ALLAN TRAN",
		"ANGELIKA RYE",
		"KENT SUTER",
		"SCOTT HANCOCK",
		"STEVEN VAN DER GRAAF-MASTERS",
		"AIE MELBOURNE",
		"USER TESTERS",
		"SOUNDSNAP"
	};

	private void Awake()
	{
		_title.text = "";
		_name.text = "";
		_audioSource = GetComponent<AudioSource>();
		Time.timeScale = 1;
		_exitText.text = "";
	}

	public void NextName()
	{
		_audioSource.PlayOneShot(_scanClip);
		if (_currentName < _names.Length)
		{
			_title.text = _titles[_currentName];
			_name.text = _names[_currentName];
			_currentName++;
		}
		else
		{
			_name.text = "";
			_title.text = $"SUBJECT #{_subjectNumber++}";
			_exitText.text = "Press ENTER to Return";
		}
	}

	private void Update()
	{
		if (Keyboard.current.enterKey.wasReleasedThisFrame)
		{
			LevelManager.Instance.LoadScene("MainMenu");
		}
	}
}
