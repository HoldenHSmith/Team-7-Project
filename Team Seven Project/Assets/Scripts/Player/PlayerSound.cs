using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
	[SerializeField] List<AudioClip> _footstepClips = new List<AudioClip>();
	[SerializeField] List<AudioClip> _runningClips = new List<AudioClip>();
	[Range(0, 2), SerializeField] private float _footStepPitchMin = 0.5f;
	[Range(0, 2), SerializeField] private float _footStepPitchMax = 1.5f;
	[SerializeField] AudioClip _keycardUsedClip = null;
	[SerializeField] AudioClip _interactClip = null;

	private AudioSource _audioSource;
	private float _volume = 0;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		_volume = _audioSource.volume;
	}

	public void PlayFootstep()
	{
		_audioSource.volume = _volume;
		if (_footstepClips.Count <= 0)
			return;
		_audioSource.pitch = Random.Range(_footStepPitchMin, _footStepPitchMax);
		int index = Random.Range(0, _footstepClips.Count);
		_audioSource.PlayOneShot(_footstepClips[index]);
		_audioSource.pitch = 1;

	}

	public void PlayFootstepRun()
	{
		_audioSource.volume = _volume;
		if (_runningClips.Count <= 0)
			return;
		_audioSource.pitch = Random.Range(_footStepPitchMin, _footStepPitchMax);
		int index = Random.Range(0, _runningClips.Count);
		_audioSource.PlayOneShot(_runningClips[index]);
		_audioSource.pitch = 1;

	}

	public void PlayKeycardUsed()
	{
		_audioSource.volume = 0.20f;
		_audioSource.pitch = 1;
		_audioSource.PlayOneShot(_keycardUsedClip);

	}

	public void PlayInteract()
	{
		_audioSource.volume = 0.10f;
		_audioSource.pitch = 1;
		_audioSource.PlayOneShot(_interactClip);
	}
}
