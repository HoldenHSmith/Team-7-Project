using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuHandler : MonoBehaviour
{
	[SerializeField] private Button _newGameButton = null;
	[SerializeField] private Button _continueButton = null;
	[SerializeField] private Button _settingsButton = null;
	[SerializeField] private Button _quitButton = null;

	[SerializeField] private Color _enabledText = Color.white;
	[SerializeField] private Color _disabledText = Color.white;
	[SerializeField] private string _sceneNameToLoad = "";

	private void Awake()
	{
		Time.timeScale = 1;
		_continueButton.interactable = SaveManager.SaveExists();

		SetTextColor(_newGameButton);
		SetTextColor(_continueButton);
		SetTextColor(_settingsButton);
		SetTextColor(_quitButton);
		//Camera.main.aspect = (Screen.width / Screen.height);
	}


	private void SetTextColor(Button button)
	{
		if (button == null)
			return;

		TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();

		if (button.interactable)
			text.color = _enabledText;
		else
			text.color = _disabledText;

	}

	public void NewGame()
	{
		SaveManager.ClearSave();
		SceneManager.LoadScene(_sceneNameToLoad);
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

}
