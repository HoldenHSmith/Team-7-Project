﻿using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.InputSystem;

public class MainMenuHandler : MonoBehaviour
{
	[SerializeField] private string _sceneNameToLoad = "";
	[SerializeField] private CinemachineVirtualCamera _startCamera = null;
	[SerializeField] private CinemachineVirtualCamera _mainMenuCamera = null;
	[SerializeField] private CinemachineVirtualCamera _settingsCamera = null;
	[SerializeField] private CinemachineVirtualCamera _playCamera = null;

	private CinemachineTrackedDolly _mainDolly;
	private CinemachineTrackedDolly _settingsDolly;
	private CinemachineTrackedDolly _playDolly;

	private MenuState _menuState = MenuState.Start;
	private float _playAcceleration = 5;
	[SerializeField] private TextMeshProUGUI _startText = null;

	private void Awake()
	{
		Time.timeScale = 1;
		//Camera.main.aspect = (Screen.width / Screen.height);
		_mainDolly = _mainMenuCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
		_settingsDolly = _settingsCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
		_playDolly = _playCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
	}

	private void Update()
	{
		_startText.enabled = false;
		switch (_menuState)
		{
			case MenuState.Start:
				_startText.enabled = true;
				HandleStart();
				break;
			case MenuState.Main:
				HandleMain();
				break;
			case MenuState.Settings:
				break;
			case MenuState.Play:
				HandlePlay();
				break;
			default:
				break;
		}
	}

	private void HandleStart()
	{
		_startCamera.Priority = 100;
		if (Keyboard.current.enterKey.wasReleasedThisFrame)
		{
			_mainMenuCamera.Priority = 0;
			_menuState = MenuState.Main;
			_mainDolly.m_PathPosition = 0;
		}
	}

	private void HandleMain()
	{
		_mainDolly.m_PathPosition += Time.unscaledDeltaTime * 2;
		_mainMenuCamera.Priority = 100;
	}

	private void HandleSettings()
	{

	}

	private void HandlePlay()
	{
		_playAcceleration += Time.unscaledDeltaTime;
		_mainMenuCamera.Priority = 0;
		_playCamera.Priority = 100;
		_playDolly.m_PathPosition += Time.unscaledDeltaTime * _playAcceleration;
	}

	public void NewGameClicked()
	{
		_menuState = MenuState.Play;
		SaveManager.ClearSave();
	}

	public void LoadGame()
	{
		SceneManager.LoadSceneAsync(_sceneNameToLoad);
	}

	public void ContinueGame()
	{
		SceneManager.LoadScene(_sceneNameToLoad);
	}

	public void SettingsMenu()
	{

	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public MenuState CurrentState { get => _menuState; set => _menuState = value; }

}

public enum MenuState
{
	Start,
	Main,
	Settings,
	Play
}
