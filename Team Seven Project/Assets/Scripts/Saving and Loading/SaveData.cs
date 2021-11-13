using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
	public KeySave[] KeySaves;
	public SaveVector3 PlayerPosition;
	public SaveQuaternion PlayerRotation;
	public bool[] DoorsUnlocked;
	public bool[] MiniKeycardsCollected;
	public bool[] MiniKeycardDoorsUnlocked;
	public int CurrentMiniKeycards;

	public SaveData(Dictionary<AreaType, bool> keyValues, bool[] miniKeycardsCollected, Vector3 playerPosition, Quaternion rotation, List<bool> doorsUnlocked, int currentMiniKeycards, bool[] miniKeycardDoorsUnlocked)
	{
		KeySaves = new KeySave[keyValues.Count];

		for (int i = 0; i < keyValues.Count; i++)
		{
			KeySaves[i] = new KeySave((AreaType)i, keyValues[(AreaType)i]);
		}

		MiniKeycardsCollected = miniKeycardsCollected;
		PlayerPosition = new SaveVector3(playerPosition);
		PlayerRotation = new SaveQuaternion(rotation);
		DoorsUnlocked = doorsUnlocked.ToArray();
		CurrentMiniKeycards = currentMiniKeycards;
		MiniKeycardDoorsUnlocked = miniKeycardDoorsUnlocked;
	}

	public Vector3 GetPosition()
	{
		return new Vector3(PlayerPosition.X, PlayerPosition.Y, PlayerPosition.Z);
	}

	public Quaternion GetRotation()
	{
		return new Quaternion(PlayerRotation.X, PlayerRotation.Y, PlayerRotation.Z, PlayerRotation.W);
	}

	public List<bool> DoorStatusesToList()
	{
		List<bool> statuses = new List<bool>();

		for (int i = 0; i < DoorsUnlocked.Length; i++)
		{
			statuses.Add(DoorsUnlocked[i]);
		}

		return statuses;

	}

	public Dictionary<AreaType, bool> KeyDict()
	{
		Dictionary<AreaType, bool> dict = new Dictionary<AreaType, bool>();

		for (int i = 0; i < KeySaves.Length; i++)
		{
			dict.Add(KeySaves[i].Area, KeySaves[i].Unlocked);
		}

		return dict;
	}

}

[Serializable]
public struct KeySave
{
	public AreaType Area;
	public bool Unlocked;

	public KeySave(AreaType area, bool unlocked)
	{
		Area = area;
		Unlocked = unlocked;
	}
}

[Serializable]
public struct SaveVector3
{
	public float X;
	public float Y;
	public float Z;

	public SaveVector3(float x, float y, float z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public SaveVector3(Vector3 vec3)
	{
		X = vec3.x;
		Y = vec3.y;
		Z = vec3.z;
	}
}

[Serializable]
public struct SaveQuaternion
{
	public float X;
	public float Y;
	public float Z;
	public float W;

	public SaveQuaternion(float x, float y, float z, float w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	public SaveQuaternion(Quaternion q)
	{
		X = q.x;
		Y = q.y;
		Z = q.z;
		W = q.w;

	}
}
