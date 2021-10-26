using UnityEngine;

public class BeakerSource : MonoBehaviour, IInteractable
{
	public void OnInteract(PlayerCharacter playerCharacter)
	{
		playerCharacter.HasBeaker = true;
		Debug.Log("You got a beaker!");
	}
}
