using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{
	[SerializeField] private bool _paused = false;
	[SerializeField] private GameObject _pauseMenu = null;
	[SerializeField] private string _mainMenuSceneName = "";

	public void TogglePauseMenu()
	{
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

	}

	public void MainMenu()
	{
		SceneManager.LoadScene(_mainMenuSceneName);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
