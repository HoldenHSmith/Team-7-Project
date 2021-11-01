using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

public class SaveManager
{
	//Singleton
	private static readonly SaveManager _instance = new SaveManager();

	private static string _directory = "Data";
	private static string _fileName = "SaveData";
	private static SaveData _currentSaveData;

	public static void Save()
	{
		_currentSaveData = new SaveData(CollectionManager.Instance.KeysCollected, GameManager.Instance.Player.transform.position,DoorManager.GetLockedStatuses());

		//Check if the directory exists, if not, create it
		if (!DirectoryExists())
			Directory.CreateDirectory(Application.persistentDataPath + "/" + _directory);

		//Serializes the data to binary and writes it to the file
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = File.Create(GetFullPath());
		formatter.Serialize(file, _currentSaveData);
		file.Close();
	}

	public static bool Load()
	{
		SaveData saveData = null;

		if(SaveExists())
		{
			try
			{
				//Deserializes data from binary and loads it
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream file = File.Open(GetFullPath(), FileMode.Open);
				saveData = (SaveData)formatter.Deserialize(file);
				file.Close();

			}
			catch(SerializationException e)
			{
				Debug.Log($"Error Loading File: {e.Message}");
			}
		}

		_currentSaveData = saveData;
		return (_currentSaveData!= null) ? true : false;
	}

	public static void ClearSave()
	{
		if(SaveExists())
		{
			try
			{
				File.Delete(GetFullPath());
			}
			catch(SerializationException e)
			{
				Debug.Log($"Error Deleting File: {e.Message}");
			}
		}
	}

	public static void CreateNewSave(Vector3 position,Dictionary<AreaType,bool> keyValues )
	{
		_currentSaveData = new SaveData(keyValues,position,DoorManager.GetLockedStatuses());
		
	}

	private static bool SaveExists()
	{
		return File.Exists(GetFullPath());
	}

	private static bool DirectoryExists()
	{
		return Directory.Exists(Application.persistentDataPath + "/" + _directory);
	}

	private static string GetFullPath()
	{
		return Application.persistentDataPath + "/" + _directory + "/" + _fileName +  ".dat";
	}

	public static SaveManager Instance { get => _instance; }
	public SaveData Current { get => _currentSaveData; }
}
