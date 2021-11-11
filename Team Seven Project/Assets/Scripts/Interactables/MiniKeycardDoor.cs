using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniKeycardDoor : MonoBehaviour
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
}
