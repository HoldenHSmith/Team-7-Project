using UnityEngine;

public class Keycard : MonoBehaviour, ICollectable, IInteractable
{
	[SerializeField] private AreaType _area;

	public void OnCollect()
	{
		CollectionManager.Instance.SetKeyValue(_area, true);
	}

	public void OnInteract(PlayerCharacter playerCharacter)
	{
		OnCollect();
		this.gameObject.SetActive(false);
	}
}

