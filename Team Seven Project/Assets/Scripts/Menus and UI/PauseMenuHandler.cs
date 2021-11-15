using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private bool _paused = false;
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _pauseMain = null;
    [SerializeField] private GameObject _settingsMenu = null;
    [SerializeField] private string _mainMenuSceneName = "";
    private PauseMenuState _state = PauseMenuState.Main;
    private bool _toggledThisFrame = false;
    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        _toggledThisFrame = true;
        if (_paused)
        {
            _paused = false;
            Time.timeScale = 1;

            if (_pauseMenu != null)
                _pauseMenu.SetActive(false);
        }
        else
        {
            _paused = true;
            Time.timeScale = 0;

            if (_pauseMenu != null)
                _pauseMenu.SetActive(true);
        }


    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && _paused && !_toggledThisFrame)
        {
            switch (_state)
            {
                case PauseMenuState.Main:
                    TogglePauseMenu();
                    break;
                case PauseMenuState.Settings:
                    ToggleSettings();
                    break;

            }
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && !_paused)
        {
            _state = PauseMenuState.Main;
            TogglePauseMenu();
        }

        //rewrite this junk
        if (_paused)
            _pauseMenu.SetActive(true);
        else
            _pauseMenu.SetActive(false);

        if (_paused && _state == PauseMenuState.Main)
        {
            _pauseMain.SetActive(true);
            _settingsMenu.SetActive(false);
            _state = PauseMenuState.Main;
        }
        else if (_paused && _state == PauseMenuState.Settings)
        {
            _settingsMenu.SetActive(true);
            _pauseMain.SetActive(false);
            _state = PauseMenuState.Settings;
        }


        _toggledThisFrame = false;
    }

    public void ToggleSettings()
    {
        if (_state == PauseMenuState.Main)
        {
            _settingsMenu.SetActive(true);
            _pauseMenu.SetActive(false);
            _state = PauseMenuState.Settings;
        }
        else if (_state == PauseMenuState.Settings)
        {
            _settingsMenu.SetActive(false);
            _pauseMenu.SetActive(true);
            _state = PauseMenuState.Main;
        }
    }

    public void ContinueGame()
    {
        TogglePauseMenu();
    }

    public void RestartGame()
    {
        TogglePauseMenu();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void SettingsMenu()
    {
        ToggleSettings();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public enum PauseMenuState
    {
        Main,
        Settings
    }
}
