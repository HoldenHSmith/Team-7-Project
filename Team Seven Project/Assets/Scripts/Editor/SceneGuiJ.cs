using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneGuiJ : EditorWindow
{
	static Texture[] _optionIcons = new Texture[7];
	static string _assetPath = "Assets/Textures/Editor/";
	static int _optionSelected = -1;
	static GUIStyle _buttonStyle;
	static int _prevOptionSelected = -1;
	static List<GameObject> _objects = new List<GameObject>();

	private void Awake()
	{
		SceneView.duringSceneGui += OnScene;
		LoadIcons();
	}

	[MenuItem("Window/Scene GUI/Enable")]
	public static void Enable()
	{
		SceneView.duringSceneGui += OnScene;

		LoadIcons();

	}

	private static void LoadIcons()
	{
		_optionIcons[0] = (Texture2D)AssetDatabase.LoadAssetAtPath(_assetPath + "guard_icon.png", typeof(Texture2D));
		_optionIcons[1] = (Texture2D)AssetDatabase.LoadAssetAtPath(_assetPath + "camera_icon.png", typeof(Texture2D));
		_optionIcons[2] = (Texture2D)AssetDatabase.LoadAssetAtPath(_assetPath + "keycard_icon.png", typeof(Texture2D));
		_optionIcons[3] = (Texture2D)AssetDatabase.LoadAssetAtPath(_assetPath + "door_icon.png", typeof(Texture2D));
		_optionIcons[4] = (Texture2D)AssetDatabase.LoadAssetAtPath(_assetPath + "steel_door_icon.png", typeof(Texture2D));
		_optionIcons[5] = (Texture2D)AssetDatabase.LoadAssetAtPath(_assetPath + "save_icon.png", typeof(Texture2D));
		_optionIcons[6] = (Texture2D)AssetDatabase.LoadAssetAtPath(_assetPath + "cancel_icon.png", typeof(Texture2D));
	}

	[MenuItem("Window/Scene GUI/Disable")]
	public static void Disable()
	{
		SceneView.duringSceneGui -= OnScene;

	}

	private static void OnScene(SceneView sceneview)
	{
		Handles.BeginGUI();
		GUILayout.BeginHorizontal();
		{
			GUILayout.Space(12);
			_buttonStyle = new GUIStyle(GUI.skin.button);
			_buttonStyle.margin = new RectOffset(0, 0, _buttonStyle.margin.top, _buttonStyle.margin.bottom);
			GUILayout.BeginVertical(GUILayout.MaxWidth(35));
			{
				_optionSelected = GUILayout.SelectionGrid(_optionSelected, _optionIcons, 1, _buttonStyle, GUILayout.Width(32), GUILayout.Height(32 * 6));
				if (_optionSelected == 6)
					_optionSelected = -1;

			}
			GUILayout.EndVertical();
		}
		GUILayout.EndHorizontal();

		Handles.EndGUI();

		HandleSelection();
		_prevOptionSelected = _optionSelected;
	}

	private static void HandleSelection()
	{
		if (_optionSelected == -1)
		{
			if (_prevOptionSelected != _optionSelected)
			{
				RepaintSceneView();
				Selection.objects = null;
			}
			return;
		}

		
		if (_prevOptionSelected != _optionSelected)
		{
			RepaintSceneView();
			switch (_optionSelected)
			{
				case 0:
					var enemyObjects = (Enemy[])FindObjectsOfType(typeof(Enemy));
					_objects = new List<GameObject>();
					for (int i = 0; i < enemyObjects.Length; i++)
					{
						_objects.Add(enemyObjects[i].gameObject);
					}
					break;

				case 1:
					var cameraObjects = (GuardCamera[])FindObjectsOfType(typeof(GuardCamera));
					_objects = new List<GameObject>();
					for (int i = 0; i < cameraObjects.Length; i++)
					{
						_objects.Add(cameraObjects[i].gameObject);
					}
					break;
				case 2:
					var keycardObjects = (Keycard[])FindObjectsOfType(typeof(Keycard));
					var miniKeycardObjects = (MiniKeycard[])FindObjectsOfType(typeof(MiniKeycard));

					_objects = new List<GameObject>();
					for (int i = 0; i < keycardObjects.Length; i++)
					{
						_objects.Add(keycardObjects[i].gameObject);
					}

					for (int i = 0; i < miniKeycardObjects.Length; i++)
					{
						_objects.Add(miniKeycardObjects[i].gameObject);
					}

					break;
				case 3:
					var keycardDoorObjects = (KeycardDoor[])FindObjectsOfType(typeof(KeycardDoor));
					_objects = new List<GameObject>();
					for (int i = 0; i < keycardDoorObjects.Length; i++)
					{
						_objects.Add(keycardDoorObjects[i].gameObject);
					}
					break;
				case 4:
					var miniKeycardDoorObjects = (MiniKeycardDoor[])FindObjectsOfType(typeof(MiniKeycardDoor));
					_objects = new List<GameObject>();
					for (int i = 0; i < miniKeycardDoorObjects.Length; i++)
					{
						_objects.Add(miniKeycardDoorObjects[i].gameObject);
					}
					break;
				case 5:
					var checkpointObjects = (Checkpoint[])FindObjectsOfType(typeof(Checkpoint));
					_objects = new List<GameObject>();
					for (int i = 0; i < checkpointObjects.Length; i++)
					{
						_objects.Add(checkpointObjects[i].gameObject);
					}
					break;
			}
			if (_objects != null && _objects.Count > 0)
				Selection.objects = _objects.ToArray();
		}

		if (_objects != null && _objects.Count > 0)
		{
			for (int i = 0; i < _objects.Count; i++)
			{
				Handles.color = Color.yellow;
				if (_objects[i] != null)
					HandlesJ.DrawDirectionalLine(_objects[i].transform.position + (Vector3.up * 25), _objects[i].transform.position, Color.yellow);
			}
		}


	}

	private static void RepaintSceneView()
	{
		EditorWindow view = EditorWindow.GetWindow<SceneView>();
		view.Repaint();
	}


}
