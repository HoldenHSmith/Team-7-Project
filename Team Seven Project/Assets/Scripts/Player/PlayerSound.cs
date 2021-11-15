using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
	[SerializeField] List<AudioClip> _footstepClips = new List<AudioClip>();
	[Range(0, 2), SerializeField] private float _footStepPitchMin = 0.5f;
	[Range(0, 2), SerializeField] private float _footStepPitchMax = 1.5f;
	
	private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayFootstep()
	{
		if (_footstepClips.Count <= 0)
			return;
		_audioSource.pitch = Random.Range(_footStepPitchMin, _footStepPitchMax);
		int index = Random.Range(0, _footstepClips.Count);
		_audioSource.PlayOneShot(_footstepClips[index]);
		_audioSource.pitch = 1;

	}
}
