using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class KeycardManager : MonoBehaviour
{
	[SerializeField] private Keycard[] _keycards;
	[SerializeField] private MiniKeycard[] _miniKeycards;

	//public static void RegisterKeycard(Keycard keycard)
	//{
	//	if (_keycards.Contains(keycard))
	//		return;

	//	_keycards.Add(keycard);
	//}

	//public static void RemoveKeycard(Keycard keycard)
	//{
	//	if (!_keycards.Contains(keycard))
	//		return;

	//	_keycards.Remove(keycard);
	//}

	//public static void RegisterMiniKeycard(MiniKeycard keycard)
	//{
	//	if (_miniKeycards.Contains(keycard))
	//		return;

	//	_miniKeycards.Add(keycard);
	//}

	//public static void RemoveMiniKeycard(MiniKeycard keycard)
	//{
	//	if (!_miniKeycards.Contains(keycard))
	//		return;

	//	_miniKeycards.Remove(keycard);
	//}

	public void LoadKeycards(Dictionary<AreaType, bool> keycardValues)
	{
		for (int i = 0; i < _keycards.Length; i++)
		{
			_keycards[i].SetCollected(keycardValues[_keycards[i].Area]);
		}
	}

	public void LoadMinikeycards(bool[] values)
	{
		for (int i = values.Length - 1; i >= 0; i--)
		{
			_miniKeycards[i].Collected = values[i];
			_miniKeycards[i].LoadCollected(values[i]);
		}

	}

	public  Keycard[] Keycards { get => _keycards; set => _keycards = value; }
	public MiniKeycard[] MiniKeycards { get => _miniKeycards; set => _miniKeycards = value; }

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		foreach (Keycard obj in _keycards)
		{
			Handles.DrawAAPolyLine(transform.position, obj.transform.position);
		}
	}
#endif
}
