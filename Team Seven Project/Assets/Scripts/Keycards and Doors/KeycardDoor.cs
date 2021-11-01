using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoor : MonoBehaviour, IInteractable
{
	[SerializeField] private bool _unlocked = false;
	[SerializeField] private AreaType _area = AreaType.Containment;

	private Animator _animator;
	private int _openHash;

	private void Awake()
	{
		_openHash = Animator.StringToHash("Open");
		_animator = GetComponentInChildren<Animator>();
	}

	public void OnInteract(PlayerCharacter playerCharacter)
	{
		if(CollectionManager.Instance.CheckKeyCollected(_area))
		{
			Unlock();	
		}
	}

	public void Unlock()
	{
		_unlocked = true;
		_animator.SetTrigger(_openHash);
	}
}
