using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlaySound : MonoBehaviour
{
	public List<AudioClip> SoundClips;
	private CharacterInput _input;
	private AudioSource _audioSource;

	private int _index = 0;

	private void Awake()
	{
		if (SoundClips == null)
			SoundClips = new List<AudioClip>();

		_audioSource = GetComponent<AudioSource>();
		_input = new CharacterInput();

		//_input.Player.Test.started += ctx => OnInputButton(ctx);
	}

	void OnInputButton(InputAction.CallbackContext context)
	{
		if (_index > SoundClips.Count - 1)
			_index = 0;
		if (_audioSource.isPlaying)
			_audioSource.Stop();

		_audioSource.PlayOneShot(SoundClips[_index]);
		_index++;
	}

	private void OnEnable()
	{
		_input.Enable();
	}

	private void OnDisable()
	{
		_input.Disable();
	}
}
