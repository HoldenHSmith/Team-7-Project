using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer = null;
    [SerializeField] private Toggle _fullscreenToggle = null;
    [SerializeField] private TMP_Dropdown _resolutionDropdown = null;
    [SerializeField] private TMP_Dropdown _qualityDropdown = null;
    [SerializeField] private TMP_Dropdown _vSyncDropdown = null;

    [SerializeField] private Slider _masterAudio = null;
    [SerializeField] private Slider _ambienceAudio = null;
    [SerializeField] private Slider _effectsAudio = null;
    [SerializeField] private Slider _gammaSlider = null;

    [SerializeField] private Volume _volume = null;



    private LiftGammaGain _gamma;

    private string _aaPrefStr = "_aa";
    private string _resPrefStr = "_res";
    private string _vSyncPrefStr = "_vsync";
    private string _fullscreenPrefStr = "_fs";
    private string _masterAudPrefStr = "_maud";
    private string _ambientAudPrefStr = "_aaud";
    private string _effectsAudPrefStr = "_eaud";
    private string _gammaPrefStr = "_gamma";


    private Resolution[] _resolutions = null;
    private Vector2 _selectedResolution;

    private void Awake()
    {
        _fullscreenToggle.isOn = Screen.fullScreen;
        GetResolutions();
        SetAspectRatio();

        //float audioValue;
        //_audioMixer.GetFloat("MasterVolume", out audioValue);
        _masterAudio.value = 0;
        //_audioMixer.GetFloat("MusicVolume", out audioValue);
        _ambienceAudio.value = 0;
        // _audioMixer.GetFloat("SoundEffectsVolume", out audioValue);
        _effectsAudio.value = 0;
        LoadSettings();

        if (_volume.profile.TryGet(out _gamma))
        {
            Debug.Log("Found Gamma Setting");
        }
        else
        {
            Debug.LogError("Couldnt find Gamma setting");
        }
    }

    private void GetResolutions()
    {
        _resolutionDropdown.ClearOptions();

        _resolutions = Screen.resolutions;
        Resolution[] resolutions = Screen.resolutions;
        List<Resolution> uniqueRes = new List<Resolution>();
        List<string> options = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].ToString();
            option = option.Replace("@", "");
            option = option.Substring(0, option.Length - 6);

            if (!options.Contains(option))
            {
                options.Add(option);
                uniqueRes.Add(resolutions[i]);
            }



        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolution;
        _resolutionDropdown.RefreshShownValue();
        _resolutions = uniqueRes.ToArray();
        OnResolutionChanged();

    }

    private void SetAspectRatio()
    {
        //Camera.main.aspect = (Screen.width / Screen.height);
    }

    private bool CheckIfCurrentResolution(int index)
    {
        if (_resolutions[index].width == Screen.width && _resolutions[index].height == Screen.height)
            return true;

        return false;
    }

    public void OnFullscreenChanged()
    {
        Screen.fullScreen = _fullscreenToggle.isOn;
    }

    public void OnResolutionChanged()
    {
        Resolution selected = _resolutions[_resolutionDropdown.value];
        Screen.SetResolution(selected.width, selected.height, Screen.fullScreen);
    }

    public void OnQualityChanged()
    {
        QualitySettings.SetQualityLevel(_qualityDropdown.value);
    }

    private float CalculateLogValue(float rawValue)
    {
        return Mathf.Log10(rawValue) * 20;
    }


    public void OnVSyncChanged()
    {
        QualitySettings.vSyncCount = _vSyncDropdown.value;
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

	public void OnGammaChange()
    {
        if (_gamma != null)
        {
            _gamma.gamma.Override(new Vector4(1f, 1f, 1f, _gammaSlider.value));
        }
    }

    public void SaveSettings()
    {


        PlayerPrefs.SetInt(_resPrefStr, _resolutionDropdown.value);
        PlayerPrefs.SetInt(_vSyncPrefStr, _vSyncDropdown.value);
        PlayerPrefs.SetInt(_fullscreenPrefStr, BoolToInt(_fullscreenToggle.isOn));
        PlayerPrefs.SetFloat(_masterAudPrefStr, _masterAudio.value);
        PlayerPrefs.SetFloat(_ambientAudPrefStr, _ambienceAudio.value);
        PlayerPrefs.SetFloat(_effectsAudPrefStr, _effectsAudio.value);
        PlayerPrefs.SetFloat(_gammaPrefStr, _gammaSlider.value);
    }


    public void LoadSettings()
    {
        _resolutionDropdown.value = PlayerPrefs.GetInt(_resPrefStr, _resolutions.Length - 1);
        _vSyncDropdown.value = PlayerPrefs.GetInt(_vSyncPrefStr, 0);
        _fullscreenToggle.isOn = IntToBool(PlayerPrefs.GetInt(_fullscreenPrefStr, 1));
        _masterAudio.value = PlayerPrefs.GetFloat(_masterAudPrefStr, 1);
        _ambienceAudio.value = PlayerPrefs.GetFloat(_ambientAudPrefStr, 1);
        _effectsAudio.value = PlayerPrefs.GetFloat(_effectsAudPrefStr, 1);
        _gammaSlider.value = PlayerPrefs.GetFloat(_gammaPrefStr, 0);

        OnSoundEffectsAudioChanged();
        OnAmbienceAudioChanged();
        OnMasterAudioChanged();
        OnResolutionChanged();
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

    private void OnDisable()
    {
        SaveSettings();
    }


}
