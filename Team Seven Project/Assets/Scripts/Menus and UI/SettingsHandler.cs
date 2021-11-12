using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsHandler : MonoBehaviour
{
	[SerializeField] private Toggle _fullscreenToggle = null;
	[SerializeField] private TMP_Dropdown _resolutionDropdown = null;
	[SerializeField] private TMP_Dropdown _qualityDropdown = null;
	[SerializeField] private TMP_Dropdown _aaDropdown = null;
	[SerializeField] private TMP_Dropdown _vSyncDropdown = null;
	[SerializeField] private Slider _audioSlider = null;

	[SerializeField] private Resolution[] _resolutions = null;
	private Vector2 _selectedResolution;

	private void Awake()
	{
		GetResolutions();
		SetAspectRatio();
		_fullscreenToggle.isOn = Screen.fullScreen;
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
			option = option.Substring(0, option.Length - 6);
			option = option.Trim();

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
		Camera.main.aspect = (Screen.width / Screen.height);
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
		PlayerPrefs.SetFloat("Master Volum", _audioSlider.value);
	}
}
