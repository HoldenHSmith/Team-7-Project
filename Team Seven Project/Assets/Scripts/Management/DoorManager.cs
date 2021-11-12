using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

	[SerializeField] private KeycardDoor[] _doors;
	[SerializeField] private MiniKeycardDoor[] _miniDoors;


	public DoorManager()
	{

	}

	//public static void RegisterDoor(KeycardDoor door)
	//{
	//	if (_doors.Contains(door))
	//		return;

	//	_doors.Add(door);

	//}

	//public static void RemoveDoor(KeycardDoor door)
	//{
	//	if (!_doors.Contains(door))
	//		return;

	//	_doors.Remove(door);
	//}

	//public static void RegisterMiniDoor(MiniKeycardDoor door)
	//{
	//	if (_miniDoors.Contains(door))
	//		return;

	//	_miniDoors.Add(door);
	//}

	//public static void RemoveMiniDoor(MiniKeycardDoor door)
	//{
	//	if (!_miniDoors.Contains(door))
	//		return;

	//	_miniDoors.Remove(door);
	//}

	public List<bool> GetLockedStatuses()
	{
		List<bool> statuses = new List<bool>();
		for (int i = 0; i < _doors.Length; i++)
		{
			statuses.Add(_doors[i].Unlocked);
		}

		return statuses;
	}

	public bool[] GetMiniLockedStatuses()
	{
		bool[] values = new bool[_miniDoors.Length];

		for (int i = 0; i < _miniDoors.Length; i++)
		{
			values[i] = _miniDoors[i].Unlocked;
		}

		return values;
	}

	public void SetLockedStatuses(List<bool> statuses)
	{
		for (int i = 0; i < _doors.Length; i++)
		{
			if (i >= statuses.Count)
				break;

			_doors[i].SetUnlocked(statuses[i]);
		}
	}

	public void SetMiniLockedStatues(bool[] statuses)
	{
		for (int i = 0; i < _miniDoors.Length; i++)
		{
			if (i >= statuses.Length)
				break;

			_miniDoors[i].SetUnlocked(statuses[i]);
		}
	}

	public KeycardDoor[] Doors { get => _doors; set => _doors = value; }
	public MiniKeycardDoor[] MiniDoors { get => _miniDoors; set => _miniDoors = value; }

}
