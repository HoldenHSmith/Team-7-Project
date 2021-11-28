using UnityEngine;

public class MiniKeycard : MonoBehaviour, ICollectable, IInteractable
{
	private bool _collected = false;

	public void OnCollect()
	{
		GameManager.Instance.CollectionManager.SetMiniKeycardValue(this, true);
		_collected = true;
		GameManager.Instance.OverlayHandler.DisplayToolTip("You picked up a Mini Keycard");
	}

	public bool OnInteract(PlayerCharacter playerCharacter)
	{
		OnCollect();

		this.gameObject.SetActive(false);
		playerCharacter.MiniKeycards++;
		return true;
	}

	public void SetCollected(bool collected)
	{
		if (collected)
			OnInteract(GameManager.Instance.Player);

	}

	public void LoadCollected(bool collected)
	{
		if (collected)
			this.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		//KeycardManager.RegisterMiniKeycard(this);
	}

	private void OnDisable()
	{
		//KeycardManager.RemoveMiniKeycard(this);
	}

	public bool Collected { get => _collected; set => _collected = value; }
}
