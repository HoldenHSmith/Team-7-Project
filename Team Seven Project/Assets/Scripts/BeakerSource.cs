using UnityEngine;

public class BeakerSource : MonoBehaviour, IInteractable
{
	public bool OnInteract(PlayerCharacter playerCharacter)
	{
		playerCharacter.HasBeaker = true;
		GameManager.Instance.OverlayHandler.DisplayToolTip("You picked up a Beaker\n\nHold LMB to Throw / Click RMB to Cancel.");
		return true;
	}
}
