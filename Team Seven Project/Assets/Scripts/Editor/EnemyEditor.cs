using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
[Serializable]
public class EnemyEditor : Editor
{
	private Enemy m_Enemy;

	private void OnEnable()
	{
		m_Enemy = (Enemy)target;
		RequiresConstantRepaint();
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

	}

	private void OnSceneGUI()
	{
		if (Application.isPlaying)
			OnDrawWindow();

		SceneView.RepaintAll();
	}

	private void OnDrawWindow()
	{
		Handles.BeginGUI();
		GUILayout.BeginArea(new Rect(30, 30, 220, 740));
		{
			Rect rect = EditorGUILayout.BeginVertical();
			{
				GUI.color = Color.black;
				GUI.Box(rect, GUIContent.none);
				GUI.color = Color.white;
				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label("Enemy Information");
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					GUILayout.Space(12);
					GUILayout.Label($"Current State: {m_Enemy.StateMachine.CurrentState}");
				}
				GUILayout.EndHorizontal();

			}
			EditorGUILayout.EndVertical();
		}
		GUILayout.EndArea();
		Handles.EndGUI();
	}
}
