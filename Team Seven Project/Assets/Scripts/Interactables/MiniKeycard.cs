using UnityEngine;

public class MiniKeycard : MonoBehaviour, ICollectable, IInteractable
{
	private bool _collected = false;

	public void OnCollect()
	{
		CollectionManager.Instance.SetMiniKeycardValue(this, true);
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

	private void OnEnable()
	{
		KeycardManager.RegisterMiniKeycard(this);
	}

	private void OnDisable()
	{
		KeycardManager.RemoveMiniKeycard(this);
	}

	public bool Collected { get => _collected; set => _collected = value; }
}
