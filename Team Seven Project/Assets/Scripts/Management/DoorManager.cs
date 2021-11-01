using System.Collections.Generic;
using UnityEngine;

public class DoorManager
{
	private static readonly DoorManager _instance = new DoorManager();

	private static List<KeycardDoor> _doors;

	public DoorManager()
	{
		_doors = new List<KeycardDoor>();
	}

	public static void RegisterDoor(KeycardDoor door)
	{
		if (_doors.Contains(door))
			return;

		_doors.Add(door);

	}

	public static void RemoveDoor(KeycardDoor door)
	{
		if (!_doors.Contains(door))
			return;

		_doors.Remove(door);
	}

	public static List<bool> GetLockedStatuses()
	{
		List<bool> statuses = new List<bool>();
		for (int i = 0; i < _doors.Count; i++)
		{
			statuses.Add(_doors[i].Unlocked);
		}

		return statuses;
	}

	public static void SetLockedStatuses(List<bool> statuses)
	{
		for (int i = 0; i < _doors.Count; i++)
		{
			if (i > statuses.Count)
				break;

			_doors[i].SetUnlocked(statuses[i]);
		}
	}

	public List<KeycardDoor> Doors { get => _doors; }

}
