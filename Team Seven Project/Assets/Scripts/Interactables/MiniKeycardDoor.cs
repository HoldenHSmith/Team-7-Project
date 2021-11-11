using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniKeycardDoor : MonoBehaviour, IInteractable
{
	[SerializeField] private bool _unlocked = false;
	private Animation[] _animations;
	
	private void Awake()
	{
		_animations = GetComponentsInChildren<Animation>();
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
		if (playerCharacter.MiniKeycards > 0)
		{
			playerCharacter.MiniKeycards--;

			UnlockDoor();
			return true;
		}

		return false;
	}

}
