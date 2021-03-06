using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
	[SerializeField] private Image _fadeToBlack = null;
	[SerializeField] private TextMeshProUGUI _text = null;
	[SerializeField] private float _fadeTime = 1.0f;
	[SerializeField] private AudioSource _audioSource = null;

	private GameObject _gameOverlay;

	private bool _active;
	private float _fadeTimer;
	[SerializeField] private float _timeUntilQuit = 3;
	private float _quitTimer = 0;
	private float _volume = 1;

	private void Start()
	{
		//_gameOverlay = GameManager.Instance.OverlayHandler.gameObject;
	}

	private void Update()
	{
		if (_active)
		{
			if (GameManager.Instance.OverlayHandler.gameObject.activeInHierarchy)
				GameManager.Instance.OverlayHandler.gameObject.SetActive(false);

			if (_volume > 0)
				_volume -= Time.unscaledDeltaTime / (_timeUntilQuit + _fadeTime);
			if (_volume < 0)
				_volume = 0;

			_audioSource.volume = _volume;
		}

		if (_active && _fadeTimer <= _fadeTime)
		{
			_fadeTimer += Time.deltaTime;


			float alpha = (_fadeTimer / _fadeTime);
			Color color1 = _fadeToBlack.color;
			color1.a = alpha;

			_fadeToBlack.color = color1;
			color1 = _text.color;
			color1.a = alpha;
			_text.color = color1;
		}
		else if (_fadeTimer >= _fadeTime)
		{
			Time.timeScale = 0;

			_quitTimer += Time.unscaledDeltaTime;



			if (_quitTimer >= _timeUntilQuit)
			{
				SceneManager.LoadScene("Credits");
				SaveManager.ClearSave();
			}


		}
	}

	public void Activate()
	{
		_active = true;
	}
}
