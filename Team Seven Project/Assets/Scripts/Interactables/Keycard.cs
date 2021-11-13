using UnityEngine;

public class Keycard : MonoBehaviour, ICollectable, IInteractable
{
	[SerializeField] private AreaType _area = AreaType.Containment;

	public void OnCollect()
	{
		GameManager.Instance.CollectionManager.SetKeyValue(_area, true);
	}

	public bool OnInteract(PlayerCharacter playerCharacter)
	{
		OnCollect();
		this.gameObject.SetActive(false);
		return true;
	}

	public void SetCollected(bool collected)
	{
		if (collected)
			OnInteract(GameManager.Instance.Player);
	}

	private void OnEnable()
	{
		//KeycardManager.RegisterKeycard(this);
	}

	private void OnDisable()
	{
		//KeycardManager.RemoveKeycard(this);
	}

	public AreaType Area { get => _area; }
}

