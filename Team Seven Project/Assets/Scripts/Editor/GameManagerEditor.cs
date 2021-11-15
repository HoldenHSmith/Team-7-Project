using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
	private GameManager gm;

	private void OnEnable()
	{
		gm = (GameManager)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GUILayout.BeginHorizontal();
		{
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Generate Lists"))
			{
				GenerateListOfItems();
			}
			GUILayout.FlexibleSpace();
		}
		GUILayout.EndHorizontal();
	}

	private void GenerateListOfItems()
	{
		gm.NoteManager = gm.GetComponent<NoteManager>();
		gm.KeycardManager = gm.GetComponent<KeycardManager>();
		gm.DoorManager = gm.GetComponent<DoorManager>();
		gm.NoteManager.Notes = (PaperNote[])FindObjectsOfType(typeof(PaperNote));
		gm.DoorManager.Doors = (KeycardDoor[])FindObjectsOfType(typeof(KeycardDoor));
		gm.DoorManager.MiniDoors = (MiniKeycardDoor[])FindObjectsOfType(typeof(MiniKeycardDoor));
		gm.KeycardManager.Keycards = (Keycard[])FindObjectsOfType(typeof(Keycard));
		gm.KeycardManager.MiniKeycards = (MiniKeycard[])FindObjectsOfType(typeof(MiniKeycard));
		EditorUtility.SetDirty(gm);
		EditorUtility.SetDirty(gm.NoteManager);
		EditorUtility.SetDirty(gm.DoorManager);
		EditorUtility.SetDirty(gm.KeycardManager);
		EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

	}
}
