using UnityEngine;

public class BeakerSource : MonoBehaviour, IInteractable
{
	public bool OnInteract(PlayerCharacter playerCharacter)
	{
		playerCharacter.HasBeaker = true;
		Debug.Log("You got a beaker!");
		return true;
	}
}
