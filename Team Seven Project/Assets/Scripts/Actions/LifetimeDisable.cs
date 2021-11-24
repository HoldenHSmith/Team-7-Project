using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeDisable : MonoBehaviour
{
	[SerializeField] private float _lifetime;
	private float _timer;
	private AudioSource _audioSource;

	private void Awake()
	{
		_timer = _lifetime;
		TryGetComponent(out _audioSource);
	}

	private void Update()
	{
		_timer -= Time.deltaTime;
		if (_timer <= 0)
			Destroy(this);
	}

	private void OnEnable()
	{
		_timer = _lifetime;
		if (_audioSource != null)
			_audioSource.Play();
	}
}
