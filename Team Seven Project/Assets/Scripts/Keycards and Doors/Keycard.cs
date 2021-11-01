using UnityEngine;

public class Keycard : MonoBehaviour, ICollectable, IInteractable
{
	[SerializeField] private AreaType _area = AreaType.Containment;

	public void OnCollect()
	{
		CollectionManager.Instance.SetKeyValue(_area, true);
	}

	public void OnInteract(PlayerCharacter playerCharacter)
	{
		OnCollect();
		this.gameObject.SetActive(false);
	}

	public void SetCollected(bool collected)
	{

	}
}

