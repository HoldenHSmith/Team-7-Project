using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomIntervalSound : MonoBehaviour
{
	[SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();
	[SerializeField] private float _minWaitTime = 0.25f;
	[SerializeField] private float _maxWaitTime = 2.5f;
	[SerializeField] private float _minPitch = 0.85f;
	[SerializeField] private float _maxPitch = 1.05f;
	private AudioSource _audioSource;

	private int _soundClip = 0;
	private float _waitTime = 0;
	private float _pitch = 1;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (_waitTime > 0)
			_waitTime -= Time.deltaTime;
		else
		{
			_audioSource.pitch = _pitch;
			_audioSource.clip = _audioClips[_soundClip];
			_audioSource.Play();
			RandomizeValues();
		}
	}

	private void RandomizeValues()
	{
		_soundClip = Random.Range(0, _audioClips.Count);
		_waitTime = Random.Range(_minWaitTime, _maxWaitTime);
		_pitch = Random.Range(_minPitch, _maxPitch);
	}
}
