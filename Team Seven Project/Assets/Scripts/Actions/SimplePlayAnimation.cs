using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayAnimation : MonoBehaviour
{
	[SerializeField] private Animation _animation = null;
	[SerializeField] private float _delay = 0;
	[SerializeField] private bool _playOnStart = false;

	private bool _activated = false;
	private float _delayTimer;

	private void Awake()
	{
		if (_playOnStart)
			_activated = true;
	}

	public void PlayAnimation()
	{
		if (_animation != null && _animation.clip != null && !_activated)
		{
			_delayTimer = _delay;
			_activated = true;
		}
	}

	private void Update()
	{
		if (_activated)
			_delayTimer -= Time.deltaTime;

		if (_delayTimer <= 0 && _activated)
		{
			_animation.Play();
			_activated = false;
		}
	}
}
