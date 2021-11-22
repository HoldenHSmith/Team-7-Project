using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections.Generic;

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

    private void Awake()
    {
        GetResolutions();
        SubscribeButtons();
    }

    private void SubscribeButtons()
    {
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



            _resolutionText.text = options[currentResolution];
            _resolutionSelected = currentResolution;
            _resolutionStrings = options.ToArray();
        }
    }

    private bool CheckIfCurrentResolution(int index)
    {
        if (_resolutionOptions[index].width == Screen.width && _resolutionOptions[index].height == Screen.height)
            return true;

        return false;
    }

    public void ShowGraphics()
    {
        DisableAllGroups();
        _graphicsGroup.SetActive(true);
    }

    public void ShowAudio()
    {
        DisableAllGroups();
        _audioGroup.SetActive(true);
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
    }

    public void OnFullscreenRight()
    {
        _fullscreenSelected++;
        UpdateSelected(ref _fullscreenOptions, _fullscreenSelected, out _fullscreenSelected, ref _fullscreenText);

    }

    public void OnResButtonLeft()
    {
        _resolutionSelected--;
        UpdateSelected(ref _resolutionStrings, _resolutionSelected, out _resolutionSelected, ref _resolutionText);
    }

    public void OnResButtonRight()
    {
        _resolutionSelected++;
        UpdateSelected(ref _resolutionStrings, _resolutionSelected, out _resolutionSelected, ref _resolutionText);
    }

    public void OnQualityButtonLeft()
    {
        _qualitySelected--;
        UpdateSelected(ref _qualityOptions, _qualitySelected, out _qualitySelected, ref _qualityText);
    }

    public void OnQualityButtonRight()
    {
        _qualitySelected++;
        UpdateSelected(ref _qualityOptions, _qualitySelected, out _qualitySelected, ref _qualityText);
    }

    public void OnVSyncButtonLeft()
    {
        _vsyncSelected--;
        UpdateSelected(ref _vsyncOptions, _vsyncSelected, out _vsyncSelected, ref _vSyncText);
    }

    public void OnVSyncButtonRight()
    {
        _vsyncSelected++;
        UpdateSelected(ref _vsyncOptions, _vsyncSelected, out _vsyncSelected, ref _vSyncText);
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
    }

    public void ApplyChanges()
    {
       // Screen.fullScreen = IntToBool(_fullscreenSelected);
  
        QualitySettings.SetQualityLevel(_qualitySelected);
        QualitySettings.vSyncCount = _vsyncSelected;
        Resolution selected = _resolutionOptions[_resolutionSelected];
        Screen.SetResolution(selected.width, selected.height, IntToBool(_fullscreenSelected));
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
