using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckForAudio : MonoBehaviour
{
	private AudioSource[] _sources;

	private void Start()
	{
		//Get all audio sources in the scene.
		_sources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
	}

	private void Update()
	{
		if (Keyboard.current.yKey.wasReleasedThisFrame)
		{
			foreach (AudioSource audioSource in _sources)
			{
				if (audioSource != null && audioSource.isPlaying)
				{
					if (audioSource.clip != null)
						Debug.Log($"{audioSource.name} is playing {audioSource.clip.name}");
					else
						Debug.Log($"{audioSource.name} is playing no clip");

				}
			}
		}
	}
}
