using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlaySound : MonoBehaviour
{
	private CharacterInput m_Input;
	private AudioSource m_AudioSource;
	public List<AudioClip> SoundClips;

	private int index = 0;

	private void Awake()
	{
		if (SoundClips == null)
			SoundClips = new List<AudioClip>();

		m_AudioSource = GetComponent<AudioSource>();
		m_Input = new CharacterInput();

		m_Input.Player.Test.started += ctx => OnInputButton(ctx);
	}

	void OnInputButton(InputAction.CallbackContext context)
	{
		if (index > SoundClips.Count - 1)
			index = 0;
		if (m_AudioSource.isPlaying)
			m_AudioSource.Stop();

		m_AudioSource.PlayOneShot(SoundClips[index]);
		index++;
	}

	private void OnEnable()
	{
		m_Input.Enable();
	}

	private void nDisable()
	{
		m_Input.Disable();
	}
}
