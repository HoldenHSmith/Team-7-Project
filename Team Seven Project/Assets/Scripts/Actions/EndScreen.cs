using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
	[SerializeField] private Image _fadeToBlack;
	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private float _fadeTime;

	private bool _active;
	private float _fadeTimer;

	private void Update()
	{
		if(_active && _fadeTimer <= _fadeTime)
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
		else if(_fadeTimer >= _fadeTime)
		{
			Time.timeScale = 0;
		}
	}

	public void Activate()
	{
		_active = true;
	}
}
