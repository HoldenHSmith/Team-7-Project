using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
[Serializable]
public class EnemyEditor : Editor
{
	private Enemy _enemy;

	private void OnEnable()
	{
		_enemy = (Enemy)target;
		RequiresConstantRepaint();
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

	}

	private void OnSceneGUI()
	{
		if (Application.isPlaying)
		{
			OnDrawWindow();
			OnDrawEnemyNavData();
		}

		SceneView.RepaintAll();
	}

	private void OnDrawEnemyNavData()
	{
		Vector3 navMeshDestination = _enemy.NavAgent.destination;

		
		Debug.DrawLine(_enemy.transform.position, navMeshDestination,Color.blue);
		Handles.color = Color.blue;
		Handles.DrawWireDisc(navMeshDestination,Vector3.up, 0.25f);
	}

	private void OnDrawWindow()
	{
		Handles.BeginGUI();
		GUILayout.BeginArea(new Rect(50, 30, 220, 740));
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
					GUILayout.Label($"Current State: {_enemy.EnemyStateMachine.StateCurrent}");
				}
				GUILayout.EndHorizontal();

			}
			EditorGUILayout.EndVertical();
		}
		GUILayout.EndArea();
		Handles.EndGUI();
	}
}
