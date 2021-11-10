using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointManager))]
[Serializable]
public class WaypointManagerEditor : Editor
{
	private WaypointManager m_WaypointManager;
	private List<Waypoint> m_PreviousWaypoints;
	private int m_SelectedIndex = 0;

	private bool m_ShowWaypoint = true;
	private bool m_ShowWaypointHandles = false;
	private Vector3 _prevPosition;
	private void OnEnable()
	{
		m_WaypointManager = (WaypointManager)target;

		_prevPosition = m_WaypointManager.transform.position;
		UpdateWaypoints();

	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}

	private void OnSceneGUI()
	{
		Undo.RecordObject(m_WaypointManager, m_WaypointManager.name);
		if (!Application.isPlaying)
			CheckTargetMoved();
		DrawWaypoints();

		if (!Application.isPlaying)
		{
			OnDrawWindow();
		}
	}


	private void CheckTargetMoved()
	{
		if (m_WaypointManager.transform.position != _prevPosition)
		{
			if (m_WaypointManager && m_WaypointManager.Waypoints != null && m_WaypointManager.Waypoints.Count > 0)
			{
				for (int i = 0; i < m_WaypointManager.Waypoints.Count; i++)
				{
					Waypoint waypoint = m_WaypointManager.Waypoints[i];

					waypoint.Position = m_WaypointManager.transform.position + waypoint.Offset;
				}
			}
		}

		_prevPosition = m_WaypointManager.transform.position;
	}

	private void DrawWaypoints()
	{
		if (m_WaypointManager && m_PreviousWaypoints != null && m_ShowWaypoint)
		{
			for (int i = 0; i < m_WaypointManager.Waypoints.Count; i++)
			{
				Waypoint waypoint = m_WaypointManager.Waypoints[i];

				if (i == 0)
					Handles.color = new Color(0, 1, 0, 0.25f);
				else if (i == m_WaypointManager.Waypoints.Count - 1)
					Handles.color = new Color(1, 0, 1, 0.25f);
				else
					Handles.color = new Color(0, 1, 1, 0.25f);

				DrawWaypointPosition(waypoint);

				if (m_ShowWaypointHandles)
					DrawWaypointHandle(waypoint);

				if (i < m_WaypointManager.Waypoints.Count - 1)
					DrawLineBetweenWaypoints(waypoint, m_WaypointManager.Waypoints[i + 1]);

				if (m_WaypointManager.Type == LoopType.Repeat && i == m_WaypointManager.Waypoints.Count - 1 && m_WaypointManager.Waypoints.Count > 1)
					DrawLineBetweenWaypoints(waypoint, m_WaypointManager.Waypoints[0]);

				if (CheckUpdateWaypoints(i))
					m_SelectedIndex = i;
			}
		}
	}

	private void DrawLineBetweenWaypoints(Waypoint start, Waypoint end)
	{
		HandlesJ.DrawDirectionalLine(start.Position, end.Position, new Color(1, 1, 0, 0.25f));
	}


	private void DrawWaypointPosition(Waypoint waypoint)
	{
		Handles.DrawSolidDisc(waypoint.Position, Vector3.up, 0.25f);
	}

	private void DrawWaypointHandle(Waypoint waypoint)
	{
		Handles.color = Color.yellow;
		//waypoint.Position = Handles.PositionHandle(waypoint.Position, Quaternion.identity);
		waypoint.SetOffset(m_WaypointManager.gameObject.transform.position, Handles.PositionHandle(waypoint.Position, Quaternion.identity));
	}

	private bool CheckUpdateWaypoints(int index)
	{
		if (m_WaypointManager.Waypoints.Count != m_PreviousWaypoints.Count ||
			m_WaypointManager.Waypoints[index].Position != m_PreviousWaypoints[index].Position ||
			m_WaypointManager.Waypoints[index].WaitTime != m_PreviousWaypoints[index].WaitTime)
		{
			UpdateWaypoints();
			return true;
		}
		return false;
	}

	private void UpdateWaypoints()
	{
		if (m_WaypointManager.Waypoints != null)
		{
			for (int i = 0; i < m_WaypointManager.Waypoints.Count; i++)
			{
				m_WaypointManager.Waypoints[i].Position.y = 0.1f;
			}
			m_PreviousWaypoints = m_WaypointManager.Waypoints.Select(wp => new Waypoint(wp.Position, wp.WaitTime)).ToList();
		}

		EditorUtility.SetDirty(target);
	}

	private void OnDrawWindow()
	{
		Handles.BeginGUI();

		GUILayout.BeginArea(new Rect(30, 30, 220, 740));
		GUILayout.BeginHorizontal();
		{
			Rect rect = EditorGUILayout.BeginVertical();

			GUI.color = Color.black;
			GUI.Box(rect, GUIContent.none);
			GUILayout.Space(22);
			GUILayout.BeginVertical();
			{
				GUI.color = Color.white;

				//Heading
				GUILayout.Space(4);
				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label("Waypoint Manager");
					GUILayout.Space(4);
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();

				m_ShowWaypoint = GUILayout.Toggle(m_ShowWaypoint, "Show Waypoints");
				m_ShowWaypointHandles = GUILayout.Toggle(m_ShowWaypointHandles, "Edit Positions");
				GUILayout.Space(4);
				GUILayout.BeginVertical();
				{
					GUILayout.BeginHorizontal();
					{
						GUILayout.FlexibleSpace();
						GUILayout.Label("Path Type");
						GUILayout.FlexibleSpace();
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					{
						GUILayout.FlexibleSpace();
						m_WaypointManager.Type = (LoopType)EditorGUILayout.EnumPopup(m_WaypointManager.Type, GUILayout.Width(150));
						GUILayout.FlexibleSpace();
					}
					GUILayout.EndHorizontal();

				}
				GUILayout.EndVertical();
				GUILayout.Space(4);

				GUILayout.EndHorizontal();

				GUILayout.Space(12);
				if (m_WaypointManager.Waypoints != null && m_WaypointManager.Waypoints.Count > 0 && m_SelectedIndex < m_WaypointManager.Waypoints.Count)
				{
					GUILayout.BeginVertical();
					{
						GUILayout.BeginHorizontal();
						{
							GUILayout.FlexibleSpace();
							GUILayout.Label($"WP Selected: {m_SelectedIndex + 1}");
							GUILayout.FlexibleSpace();
						}
						GUILayout.EndHorizontal();

						GUILayout.BeginHorizontal();
						{
							GUILayout.FlexibleSpace();
							GUILayout.Label("Wait Time:");
							m_WaypointManager.Waypoints[m_SelectedIndex].WaitTime = EditorGUILayout.FloatField(m_WaypointManager.Waypoints[m_SelectedIndex].WaitTime, GUILayout.Width(45));
							GUILayout.FlexibleSpace();
						}
						GUILayout.EndHorizontal();
						GUILayout.Space(8);
						//GUILayout.BeginHorizontal();
						//{
						//	GUILayout.FlexibleSpace();
						//	GUILayout.Label("X:");
						//	m_WaypointManager.Waypoints[m_SelectedIndex].Position.x = EditorGUILayout.FloatField(m_WaypointManager.Waypoints[m_SelectedIndex].Position.x, GUILayout.Width(35));
						//	GUILayout.Label("Y:");
						//	m_WaypointManager.Waypoints[m_SelectedIndex].Position.y = EditorGUILayout.FloatField(m_WaypointManager.Waypoints[m_SelectedIndex].Position.y, GUILayout.Width(35));
						//	GUILayout.Label("Z:");
						//	m_WaypointManager.Waypoints[m_SelectedIndex].Position.z = EditorGUILayout.FloatField(m_WaypointManager.Waypoints[m_SelectedIndex].Position.z, GUILayout.Width(35));
						//	GUILayout.FlexibleSpace();
						//}
						//GUILayout.EndHorizontal();
					}
					GUILayout.EndVertical();
				}

				GUILayout.Space(12);
				GUILayout.BeginHorizontal();
				{
					if (GUILayout.Button("Add"))
					{
						AddNewWaypoint();
					}

					if (GUILayout.Button("Remove"))
					{
						RemoveWaypoint();
					}
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(22);
			}
			GUILayout.EndVertical();

			GUILayout.Space(22);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		Handles.EndGUI();
	}

	private void AddNewWaypoint()
	{
		if (m_WaypointManager.Waypoints.Count == 0)
			m_WaypointManager.AddNewWaypoint();
		else
			m_WaypointManager.InsertWaypoint(m_SelectedIndex + 1, new Waypoint(m_WaypointManager.Waypoints[m_SelectedIndex].Position + Vector3.left));

		UpdateWaypoints();
		m_SelectedIndex++;
	}

	private void RemoveWaypoint()
	{
		if (m_SelectedIndex >= 0 && m_SelectedIndex < m_WaypointManager.Waypoints.Count)
		{
			m_WaypointManager.RemoveWaypoint(m_SelectedIndex);
			UpdateWaypoints();
			m_SelectedIndex--;
		}
		else
			m_SelectedIndex = 0;

		if (m_SelectedIndex < 0)
			m_SelectedIndex = 0;
		if (m_SelectedIndex >= m_WaypointManager.Waypoints.Count)
			m_SelectedIndex = m_WaypointManager.Waypoints.Count - 1;
	}

}
