using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoor : MonoBehaviour, IInteractable
{
	[SerializeField] private bool _unlocked = false;
	[SerializeField] private AreaType _area;

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
	}
}
