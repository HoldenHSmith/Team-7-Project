using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer = null;
    [SerializeField] private Toggle _fullscreenToggle = null;
    [SerializeField] private TMP_Dropdown _resolutionDropdown = null;
    [SerializeField] private TMP_Dropdown _qualityDropdown = null;
    [SerializeField] private TMP_Dropdown _aaDropdown = null;
    [SerializeField] private TMP_Dropdown _vSyncDropdown = null;

    [SerializeField] private Slider _masterAudio = null;
    [SerializeField] private Slider _ambienceAudio = null;
    [SerializeField] private Slider _effectsAudio = null;

    private Resolution[] _resolutions = null;
    private Vector2 _selectedResolution;

    private void Awake()
    {
        GetResolutions();
        SetAspectRatio();
        _fullscreenToggle.isOn = Screen.fullScreen;

        //float audioValue;
        //_audioMixer.GetFloat("MasterVolume", out audioValue);
        _masterAudio.value = 0;
        //_audioMixer.GetFloat("MusicVolume", out audioValue);
        _ambienceAudio.value = 0;
        // _audioMixer.GetFloat("SoundEffectsVolume", out audioValue);
        _effectsAudio.value = 0;
    }

    private void GetResolutions()
    {
        _resolutionDropdown.ClearOptions();

        _resolutions = Screen.resolutions;

        List<string> options = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].ToString();
            option = option.Replace("@", "");

            if (!options.Contains(option))
            {
                options.Add(option);
            }

            if (CheckIfCurrentResolution(i))
                currentResolution = i;

        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolution;
        _resolutionDropdown.RefreshShownValue();
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

    public void OnAAChanged()
    {
        switch (_aaDropdown.value)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;
                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                break;
        }
    }

    public void OnVSyncChanged()
    {
        QualitySettings.vSyncCount = _vSyncDropdown.value;
    }

    public void OnMasterAudioChanged()
    {
        _audioMixer.SetFloat("MasterVolume", _masterAudio.value);
    }

    public void OnAmbienceAudioChanged()
    {
        _audioMixer.SetFloat("MusicVolume", _ambienceAudio.value);
    }

    public void OnSoundEffectsAudioChanged()
    {
        _audioMixer.SetFloat("SoundEffectsVolume", _effectsAudio.value);
    }

    public void SaveSettings()
    {

    }
}
