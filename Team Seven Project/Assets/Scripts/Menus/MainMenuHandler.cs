using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
	[SerializeField] private Button _newGameButton;
	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _quitButton;

	[SerializeField] private Color _enabledText;
	[SerializeField] private Color _disabledText;
	[SerializeField] private string _sceneNameToLoad;

	private void Awake()
	{
		_continueButton.interactable = SaveManager.SaveExists();

		SetTextColor(_newGameButton);
		SetTextColor(_continueButton);
		SetTextColor(_settingsButton);
		SetTextColor(_quitButton);
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
