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

	private void Awake()
	{
		_animations = GetComponentsInChildren<Animation>();


		if (!_requireKeycard && _text != null)
			_text.enabled = false;
	}

	public void UnlockDoor()
	{
		_unlocked = true;

		if (_animations != null)
		{
			foreach (Animation animation in _animations)
			{
				animation.Play();
			}
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

		if (!_unlocked && _animations != null)
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
