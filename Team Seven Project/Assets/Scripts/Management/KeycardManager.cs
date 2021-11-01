using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class KeycardManager : MonoBehaviour
{
	private static KeycardManager _instance;
	private static readonly List<Keycard> _keycards = new List<Keycard>();

	public static void RegisterKeycard(Keycard keycard)
	{
		if (_keycards.Contains(keycard))
			return;

		_keycards.Add(keycard);
	}

	public static void RemoveKeycard(Keycard keycard)
	{
		if (!_keycards.Contains(keycard))
			return;

		_keycards.Remove(keycard);
	}

	public static List<Keycard> Keycards { get => _keycards; }

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		foreach(Keycard obj in _keycards)
		{
			Handles.DrawAAPolyLine(transform.position, obj.transform.position);
		}
	}
#endif
}
