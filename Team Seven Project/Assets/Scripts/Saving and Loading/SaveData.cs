using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
	public KeySave[] KeySaves;
	public SaveVector3 PlayerPosition;
	public bool[] DoorsUnlocked;

	public SaveData(Dictionary<AreaType, bool> keyValues, Vector3 playerPosition, List<bool> doorsUnlocked)
	{
		KeySaves = new KeySave[keyValues.Count];

		for(int i = 0; i < keyValues.Count;i++)
		{
			KeySaves[i] = new KeySave((AreaType)i, keyValues[(AreaType)i]);
		}

		PlayerPosition = new SaveVector3(playerPosition);
		DoorsUnlocked = doorsUnlocked.ToArray();
	}

	public Vector3 PosToVec3()
	{
		return new Vector3(PlayerPosition.X, PlayerPosition.Y, PlayerPosition.Z);
	}

	public List<bool> DoorStatusesToList()
	{
		List<bool> statuses = new List<bool>();

		for(int i =0; i < DoorsUnlocked.Length;i++)
		{
			statuses.Add(DoorsUnlocked[i]);
		}

		return statuses;
		
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
