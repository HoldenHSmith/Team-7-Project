using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;

	[SerializeField] private GameObject _loaderCanvas = null;
	[SerializeField] private Image _progressBar = null;
	[SerializeField] private Image _backgroundImage = null;
	[SerializeField] private TextMeshProUGUI _hintText = null;
	[SerializeField, TextArea(1, 5)] private List<string> _hints = new List<string>();
	[SerializeField] private List<Sprite> _images = new List<Sprite>();
	private bool _loading = false;

	private float _target;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnLevelWasLoaded(int level)
	{
		_loaderCanvas.SetActive(false);
		_loading = false;
	}

	public async void LoadScene(string sceneName)
	{
		if (_loading == true)
			return;
		_loading = true;

		_target = 0;
		_progressBar.fillAmount = 0;
		OnNewLoad();

		var scene = SceneManager.LoadSceneAsync(sceneName);
		scene.allowSceneActivation = false;

		_loaderCanvas.SetActive(true);

		do
		{
			await Task.Delay(100);
			_target = scene.progress + 0.1f;
		} while (scene.progress < 0.9f);

		await Task.Delay(2000);
		scene.allowSceneActivation = true;

	}

	private void OnNewLoad()
	{
		int hintIndex = Random.Range(0, _hints.Count);
		int imageIndex = Random.Range(0, _images.Count);

		_hintText.text = $"Hint: {_hints[hintIndex]}";
		_backgroundImage.sprite = _images[imageIndex];
	}

	private void Update()
	{
		_progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, Time.deltaTime);
	}
}
