using System.Collections.Generic;
using UnityEngine;

public sealed class CollectionManager
{
	private static readonly CollectionManager _instance = new CollectionManager();

	private Dictionary<AreaType, bool> _keysCollected;

	public void SetKeyValue(AreaType area, bool value)
	{
		_keysCollected[area] = value;
	}

	public bool CheckKeyCollected(AreaType area)
	{
		return _keysCollected[area];
	}

	public static CollectionManager Instance { get => _instance; }
}
