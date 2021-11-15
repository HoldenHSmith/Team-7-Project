using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFadeToBlack : MonoBehaviour
{
	[SerializeField] private float _fadeTime = 1f;
	[SerializeField] private Image _image = null;

	private bool _active = false;
	private float _alpha = 0;

	void Update()
	{
		if (_active)
		{
			_alpha += Time.deltaTime / _fadeTime;
			Color color = _image.color;
			color.a = _alpha;
			_image.color = color;
		}
	}

	public void Activate()
	{
		_active = true;
	}
}
