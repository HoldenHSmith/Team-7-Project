using System.Collections.Generic;
using UnityEngine;

public sealed class CollectionManager
{
	private static readonly CollectionManager _instance = new CollectionManager();

	private Dictionary<AreaType, bool> _keysCollected;

	public CollectionManager()
	{
		_keysCollected = new Dictionary<AreaType, bool>();

		for (int i = 0; i < (int)AreaType.Count; i++)
		{
			_keysCollected.Add((AreaType)i, false);
		}

	}

	public void SetKeyValue(AreaType area, bool value)
	{
		_keysCollected[area] = value;
		Debug.Log($"Key Collected: {area}");
	}

	public bool CheckKeyCollected(AreaType area)
	{
		return _keysCollected[area];
	}

	public static CollectionManager Instance { get => _instance; }
	public Dictionary<AreaType, bool> KeysCollected { get => _keysCollected; }
	
}
