using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniKeycardDoor : MonoBehaviour, IInteractable
{
	private bool _unlocked = false;
	[SerializeField] private bool _requireKeycard = true;
	[SerializeField] private TextMeshPro _text = null;

	private Animation[] _animations;
	private AudioSource _audioSource;
	private bool _activated = false;
	[SerializeField] private float _delay = 0.5f;
	private GameObject _interactableText;

	private void Awake()
	{
		_animations = GetComponentsInChildren<Animation>();
		_audioSource = GetComponent<AudioSource>();

		if (!_requireKeycard && _text != null)
			_text.enabled = false;

		if (!UtilsJ.FindChildByName("Interactable Text", gameObject, out _interactableText))
		{
			Debug.LogWarning($"Interactable text child prefab expected on {gameObject} ");
		}
	}

	private void Update()
	{
		if (_activated)
		{
			if (_delay > 0)
				_delay -= Time.deltaTime;
			else if (_animations != null)
			{
				foreach (Animation animation in _animations)
				{
					animation.Play();
					_audioSource.Play();
				}
				_activated = false;
			}
			if (_interactableText != null)
				_interactableText.SetActive(false);

		}
	}

	public void UnlockDoor()
	{
		if (!_unlocked)
		{
			_unlocked = true;

			_activated = true;

		}
	}

	public bool OnInteract(PlayerCharacter playerCharacter)
	{
		if (playerCharacter.MiniKeycards > 0 && !_unlocked)
		{
			playerCharacter.MiniKeycards--;

			UnlockDoor();
			return true;
		}

		return false;
	}

	public void SetUnlocked(bool v)
	{

		if (!_unlocked && _animations != null && v)
			foreach (Animation anim in _animations)
			{
				anim.Play();
			}
		_unlocked = v;
	}

	private void OnEnable()
	{
		//DoorManager.RegisterMiniDoor(this);
	}

	private void OnDisable()
	{
		//DoorManager.RemoveMiniDoor(this);
	}

	public bool Unlocked { get => _unlocked; }
}
