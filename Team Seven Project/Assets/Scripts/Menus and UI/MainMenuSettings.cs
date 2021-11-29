using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections.Generic;
using UnityEngine.Audio;

public class MainMenuSettings : MonoBehaviour
{
	//Top Buttons
	[SerializeField] private Button _graphicsButton = null;
	[SerializeField] private Button _audioButton = null;
	[SerializeField] private Button _inputsButton = null;
	//Graphics

	//Fullscreen
	[SerializeField] private Button _fullscreenLeftButton = null;
	[SerializeField] private Button _fullscreenRightButton = null;
	[SerializeField] private TextMeshProUGUI _fullscreenText = null;
	private string[] _fullscreenOptions = { "Off", "On" };
	private int _fullscreenSelected = 0;

	//Resolution
	[SerializeField] private Button _resolutionLeftButton = null;
	[SerializeField] private Button _resolutionRightButton = null;
	[SerializeField] private TextMeshProUGUI _resolutionText = null;
	private string[] _resolutionStrings;
	private int _resolutionSelected = 0;

	//Quality
	[SerializeField] private Button _qualityLeftButton = null;
	[SerializeField] private Button _qualityRightButton = null;
	[SerializeField] private TextMeshProUGUI _qualityText = null;
	private string[] _qualityOptions = { "Low", "Medium", "High" };
	private int _qualitySelected = 0;
	//Vsync
	[SerializeField] private Button _vSyncLeftButton = null;
	[SerializeField] private Button _vSyncRightButton = null;
	[SerializeField] private TextMeshProUGUI _vSyncText = null;
	private string[] _vsyncOptions = { "Off", "On" };
	private int _vsyncSelected = 0;

	//Audio Settings
	[SerializeField] private Slider _masterAudio = null;
	[SerializeField] private Slider _ambienceAudio = null;
	[SerializeField] private Slider _effectsAudio = null;


	//Groups
	[SerializeField] private GameObject _graphicsGroup = null;
	[SerializeField] private GameObject _audioGroup = null;
	[SerializeField] private GameObject _inputGroup = null;


	[SerializeField] private Volume _volume = null;

	[SerializeField] private AudioMixer _audioMixer = null;

	[SerializeField] private AudioClip _clickClip = null;
	[SerializeField] private AudioSource _audioSource = null;

	private List<Resolution> _resolutionOptions = new List<Resolution>();


	private string _aaPrefStr = "_aa";
	private string _resPrefStr = "_res";
	private string _vSyncPrefStr = "_vsync";
	private string _fullscreenPrefStr = "_fs";
	private string _qualityPrefStr = "_qual";
	private string _masterAudPrefStr = "_maud";
	private string _ambientAudPrefStr = "_aaud";
	private string _effectsAudPrefStr = "_eaud";
	private string _gammaPrefStr = "_gamma";
	private float _version = 0.6f;

	public void SendMessage()
	{

	}


	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		float version = PlayerPrefs.GetFloat("_vs", 0);

		if (version != _version)
		{
			SaveManager.ClearSave();
		}

		PlayerPrefs.SetFloat("_vs", _version);
		_masterAudio.value = 0;
		_ambienceAudio.value = 0;
		_effectsAudio.value = 0;
		GetResolutions();
		SubscribeButtons();
		LoadSettings();

	}

	private void Start()
	{

		UpdateAudioAtStart();
	}

	private void UpdateAudioAtStart()
	{
		_audioMixer.SetFloat("MasterVolume", CalculateLogValue(_masterAudio.value));
		_audioMixer.SetFloat("MusicVolume", CalculateLogValue(_ambienceAudio.value));
		_audioMixer.SetFloat("SoundEffectsVolume", CalculateLogValue(_effectsAudio.value));
	}

	private void SubscribeButtons()
	{
		_graphicsButton.onClick.AddListener(ShowGraphics);
		_audioButton.onClick.AddListener(ShowAudio);
		_inputsButton.onClick.AddListener(ShowControls);

		_fullscreenLeftButton.onClick.AddListener(OnFullscreenLeft);
		_fullscreenRightButton.onClick.AddListener(OnFullscreenRight);

		_resolutionLeftButton.onClick.AddListener(OnResButtonLeft);
		_resolutionRightButton.onClick.AddListener(OnResButtonRight);

		_qualityLeftButton.onClick.AddListener(OnQualityButtonLeft);
		_qualityRightButton.onClick.AddListener(OnQualityButtonRight);

		_vSyncLeftButton.onClick.AddListener(OnVSyncButtonLeft);
		_vSyncRightButton.onClick.AddListener(OnVSyncButtonRight);
	}

	private void GetResolutions()
	{

		Resolution[] resolutions = Screen.resolutions;
		List<string> options = new List<string>();
		int currentResolution = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].ToString();
			option = option.Replace("@", "");
			option = option.Substring(0, option.Length - 6);

			if (!options.Contains(option))
			{
				options.Add(option);
				_resolutionOptions.Add(resolutions[i]);

			}


		}
		_resolutionText.text = options[currentResolution];
		_resolutionSelected = currentResolution;
		_resolutionStrings = options.ToArray();
	}

	public void ShowGraphics()
	{
		DisableAllGroups();
		_graphicsGroup.SetActive(true);
		PlayInteractSound();

	}

	public void ShowAudio()
	{
		DisableAllGroups();
		_audioGroup.SetActive(true);
		PlayInteractSound();
	}

	public void ShowControls()
	{
		DisableAllGroups();
		_inputGroup.SetActive(true);
	}

	public void DisableAllGroups()
	{
		_graphicsGroup.SetActive(false);
		_audioGroup.SetActive(false);
		_inputGroup.SetActive(false);
	}

	public void OnFullscreenLeft()
	{
		_fullscreenSelected--;
		UpdateSelected(ref _fullscreenOptions, _fullscreenSelected, out _fullscreenSelected, ref _fullscreenText);
		PlayInteractSound();
	}

	public void OnFullscreenRight()
	{
		_fullscreenSelected++;
		UpdateSelected(ref _fullscreenOptions, _fullscreenSelected, out _fullscreenSelected, ref _fullscreenText);
		PlayInteractSound();

	}

	public void OnResButtonLeft()
	{
		_resolutionSelected--;
		UpdateSelected(ref _resolutionStrings, _resolutionSelected, out _resolutionSelected, ref _resolutionText);
		PlayInteractSound();
	}

	public void OnResButtonRight()
	{
		_resolutionSelected++;
		UpdateSelected(ref _resolutionStrings, _resolutionSelected, out _resolutionSelected, ref _resolutionText);
		PlayInteractSound();
	}

	public void OnQualityButtonLeft()
	{
		_qualitySelected--;
		UpdateSelected(ref _qualityOptions, _qualitySelected, out _qualitySelected, ref _qualityText);
		PlayInteractSound();
	}

	public void OnQualityButtonRight()
	{
		_qualitySelected++;
		UpdateSelected(ref _qualityOptions, _qualitySelected, out _qualitySelected, ref _qualityText);
		PlayInteractSound();
	}

	public void OnVSyncButtonLeft()
	{
		_vsyncSelected--;
		UpdateSelected(ref _vsyncOptions, _vsyncSelected, out _vsyncSelected, ref _vSyncText);
		PlayInteractSound();
	}

	public void OnVSyncButtonRight()
	{
		_vsyncSelected++;
		UpdateSelected(ref _vsyncOptions, _vsyncSelected, out _vsyncSelected, ref _vSyncText);
		PlayInteractSound();
	}

	public void OnMasterAudioChanged()
	{
		_audioMixer.SetFloat("MasterVolume", CalculateLogValue(_masterAudio.value));
	}

	public void OnAmbienceAudioChanged()
	{
		_audioMixer.SetFloat("MusicVolume", CalculateLogValue(_ambienceAudio.value));
	}

	public void OnSoundEffectsAudioChanged()
	{
		_audioMixer.SetFloat("SoundEffectsVolume", CalculateLogValue(_effectsAudio.value));
	}

	public void OnReturn()
	{
		LoadSettings();

		PlayInteractSound();
	}

	public void PlayInteractSound()
	{
		_audioSource.pitch = Random.Range(0.95f, 1.05f);
		_audioSource.PlayOneShot(_clickClip);
	}

	private void UpdateSelected(ref string[] options, int selection, out int finalSelection, ref TextMeshProUGUI textField)
	{

		int newSelection = selection;
		if (selection < 0)
			newSelection = options.Length - 1;
		if (selection >= options.Length)
			newSelection = 0;

		textField.text = options[newSelection];
		finalSelection = newSelection;
	}

	public void SaveSettings()
	{

		PlayerPrefs.SetInt(_resPrefStr, _resolutionSelected);
		PlayerPrefs.SetInt(_vSyncPrefStr, _vsyncSelected);
		PlayerPrefs.SetInt(_fullscreenPrefStr, _fullscreenSelected);
		PlayerPrefs.SetInt(_qualityPrefStr, _qualitySelected);
		PlayerPrefs.SetFloat(_masterAudPrefStr, _masterAudio.value);
		PlayerPrefs.SetFloat(_ambientAudPrefStr, _ambienceAudio.value);
		PlayerPrefs.SetFloat(_effectsAudPrefStr, _effectsAudio.value);
	}


	public void LoadSettings()
	{
		_resolutionSelected = PlayerPrefs.GetInt(_resPrefStr, _resolutionStrings.Length - 1);
		_vsyncSelected = PlayerPrefs.GetInt(_vSyncPrefStr, _vsyncOptions.Length - 1);
		_fullscreenSelected = PlayerPrefs.GetInt(_fullscreenPrefStr, 1);
		_qualitySelected = PlayerPrefs.GetInt(_qualityPrefStr, 2);

		if (_resolutionSelected >= _resolutionStrings.Length)
			_resolutionSelected = _resolutionStrings.Length - 1;

		if (_vsyncSelected >= _vsyncOptions.Length)
			_vsyncSelected = _vsyncOptions.Length - 1;

		if (_fullscreenSelected >= _fullscreenOptions.Length)
			_fullscreenSelected = _fullscreenOptions.Length - 1;

		if (_qualitySelected >= _qualityOptions.Length)
			_qualitySelected = _qualityOptions.Length - 1;

		_resolutionText.text = _resolutionStrings[_resolutionSelected];
		_vSyncText.text = _vsyncOptions[_vsyncSelected];
		_fullscreenText.text = _fullscreenOptions[_fullscreenSelected];
		_qualityText.text = _qualityOptions[_qualitySelected];

		_masterAudio.value = PlayerPrefs.GetFloat(_masterAudPrefStr, 1);
		_ambienceAudio.value = PlayerPrefs.GetFloat(_ambientAudPrefStr, 1);
		_effectsAudio.value = PlayerPrefs.GetFloat(_effectsAudPrefStr, 1);

		ApplyChanges();

	}

	private float CalculateLogValue(float rawValue)
	{
		return Mathf.Log10(rawValue) * 20;
	}

	public void ApplyChanges()
	{
		_audioMixer.SetFloat("MasterVolume", CalculateLogValue(_masterAudio.value));
		_audioMixer.SetFloat("MusicVolume", CalculateLogValue(_ambienceAudio.value));
		_audioMixer.SetFloat("SoundEffectsVolume", CalculateLogValue(_effectsAudio.value));
		QualitySettings.SetQualityLevel(_qualitySelected);
		QualitySettings.vSyncCount = _vsyncSelected;
		Resolution selected = _resolutionOptions[_resolutionSelected];
		Screen.SetResolution(selected.width, selected.height, IntToBool(_fullscreenSelected));

		SaveSettings();
		PlayInteractSound();
	}

	public int BoolToInt(bool val)
	{
		if (val)
			return 1;
		else
			return 0;
	}

	public bool IntToBool(int val)
	{
		if (val != 0)
			return true;
		else
			return false;
	}

}
