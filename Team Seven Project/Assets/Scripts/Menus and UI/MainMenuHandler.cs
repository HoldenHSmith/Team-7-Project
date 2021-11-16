using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
	[SerializeField] private string _sceneNameToLoad = "";
	[SerializeField] private CinemachineVirtualCamera _startCamera = null;
	[SerializeField] private CinemachineVirtualCamera _mainMenuCamera = null;
	[SerializeField] private CinemachineVirtualCamera _settingsCamera = null;
	[SerializeField] private CinemachineVirtualCamera _playCamera = null;

	[SerializeField] private MenuTextMaterialBlock _newGameButton = null;
	[SerializeField] private MenuTextMaterialBlock _playGameButton = null;
	[SerializeField] private MenuTextMaterialBlock _settingsButton = null;
	[SerializeField] private MenuTextMaterialBlock _quitButton = null;
	[SerializeField] private Button _oldContinueButton = null;

	[SerializeField] private GameObject _settingsMenu = null;
	[SerializeField] private GameObject _mainMenu = null;

	private CinemachineTrackedDolly _mainDolly;
	private CinemachineTrackedDolly _settingsDolly;
	private CinemachineTrackedDolly _playDolly;
	private bool _settingsActivated = false;
	private MenuState _menuState = MenuState.Start;
	private float _playAcceleration = 5;

	private bool _settingsReverse = false;

	[SerializeField] private TextMeshProUGUI _startText = null;

	private void Awake()
	{
		Time.timeScale = 1;
		_mainDolly = _mainMenuCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
		_settingsDolly = _settingsCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
		_playDolly = _playCamera.GetCinemachineComponent<CinemachineTrackedDolly>();

		_playGameButton.CanSelect = SaveManager.SaveExists();
		if (SaveManager.SaveExists())
			_oldContinueButton.interactable = true;


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
				HandleSettings();
				break;
			case MenuState.Play:
				HandlePlay();
				break;
			default:
				break;
		}

		//For Beta Menu
		if (_settingsActivated)
		{
			_settingsMenu.SetActive(true);

			if (Keyboard.current.escapeKey.wasReleasedThisFrame)
				_settingsActivated = false;

			_mainMenu.SetActive(false);

		}
		else
		{
			_mainMenu.SetActive(true);
			_settingsMenu.SetActive(false);
		}
	}

	private void HandleStart()
	{
		_startCamera.Priority = 100;
		if (Keyboard.current.enterKey.wasReleasedThisFrame)
		{
			//_mainMenuCamera.Priority = 0;
			//_menuState = MenuState.Main;
			//_mainDolly.m_PathPosition = 0;
		}
	}

	private void HandleMain()
	{
		_mainDolly.m_PathPosition += Time.unscaledDeltaTime * 2;
		_mainMenuCamera.Priority = 100;
	}

	private void HandleSettings()
	{
		if (!_settingsReverse && _settingsDolly.m_PathPosition < _settingsDolly.m_Path.PathLength)
		{
			_settingsDolly.m_PathPosition += Time.unscaledDeltaTime * 2;
			_settingsCamera.Priority = 100;
			_mainMenuCamera.Priority = 0;
		}
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

	public void PlayGameClicked()
	{
		_menuState = MenuState.Play;
	}

	public void LoadGame()
	{
		//SceneManager.LoadSceneAsync(_sceneNameToLoad);
		LevelManager.Instance.LoadScene(_sceneNameToLoad);
	}

	public void OldNewGame()
	{
		SaveManager.ClearSave();
		ContinueGame();
	}

	public void ContinueGame()
	{
		LevelManager.Instance.LoadScene(_sceneNameToLoad);
		//SceneManager.LoadScene(_sceneNameToLoad);
	}

	public void OldSettingsClicked()
	{
		_settingsActivated = true;
	}

	public void SettingsClicked()
	{
		_menuState = MenuState.Settings;
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
