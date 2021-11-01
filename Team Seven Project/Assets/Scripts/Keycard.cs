using UnityEngine;

public class Keycard : MonoBehaviour, ICollectable
{
	[SerializeField] private AreaType _area;

	public void OnCollect()
	{
		CollectionManager.Instance.SetKeyValue(_area, true);
	}
}

