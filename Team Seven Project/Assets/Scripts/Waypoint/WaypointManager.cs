using System;
using System.Collections.Generic;
using UnityEngine;

public enum WPPathType
{
	PingPong,
	Loop,
	Once
}

[Serializable]
public class WaypointManager : MonoBehaviour
{
	public WPPathType Type;
	public bool Finished = false;
	public List<Waypoint> Waypoints;

	private bool m_Forward = true;
	private int m_CurrentIndex = 0;
	
	private void OnEnable()
	{
		if (Waypoints == null)
			Waypoints = new List<Waypoint>();
	}

	public void AddNewWaypoint()
	{
		Waypoints.Add(new Waypoint(transform.position));
	}

	public void AddWaypoint(Waypoint waypoint)
	{
		Waypoints.Add(waypoint);
	}

	public void RemoveLastWaypoint()
	{
		Waypoints.RemoveAt(Waypoints.Count - 1);
	}

	public void InsertWaypoint(int index)
	{
		Waypoints.Insert(index, new Waypoint(transform.position));
	}

	public void InsertWaypoint(int index, Waypoint waypoint)
	{
		Waypoints.Insert(index, waypoint);
	}

	public void RemoveWaypoint(Waypoint waypoint)
	{
		if (Waypoints.Contains(waypoint))
			Waypoints.Remove(waypoint);
	}

	public void RemoveWaypoint(int index)
	{
		if (Waypoints.Count > index)
			Waypoints.RemoveAt(index);
	}

	public bool GetNextWaypoint(out Waypoint waypoint)
	{
		if (Finished)
		{
			waypoint = null;
			return false;
		}
		else
		{
			switch (Type)
			{
				case WPPathType.PingPong:
					return GetNextWaypointPingPong(out waypoint);
				case WPPathType.Loop:
					return GetWaypointLoop(out waypoint);
				case WPPathType.Once:
					return GetWaypointOnce(out waypoint);

				default:
					waypoint = null;
					return false;
			}
		}
	}

	private bool GetNextWaypointPingPong(out Waypoint waypoint)
	{
		if (m_Forward)
		{
			if (m_CurrentIndex < Waypoints.Count)
			{
				waypoint = Waypoints[m_CurrentIndex];
				m_CurrentIndex++;
				return true;
			}
			else
			{
				m_Forward = false;
				m_CurrentIndex--;
				waypoint = Waypoints[m_CurrentIndex];
				return true;
			}
		}
		else
		{
			if (m_CurrentIndex >= 0)
			{
				waypoint = Waypoints[m_CurrentIndex];
				m_CurrentIndex--;
				return true;
			}
			else
			{
				m_Forward = true;
				m_CurrentIndex++;
				waypoint = Waypoints[m_CurrentIndex];
				return true;
			}
		}
	}

	private bool GetWaypointLoop(out Waypoint waypoint)
	{
		if (m_CurrentIndex < Waypoints.Count)
		{
			waypoint = Waypoints[m_CurrentIndex];
			m_CurrentIndex++;
			return true;
		}
		else
		{
			m_CurrentIndex = 0;
			waypoint = Waypoints[m_CurrentIndex];
			m_CurrentIndex++;
			return true;
		}
	}

	private bool GetWaypointOnce(out Waypoint waypoint)
	{
		if (m_CurrentIndex < Waypoints.Count)
		{
			waypoint = Waypoints[m_CurrentIndex];
			m_CurrentIndex++;
			return true;
		}
		else
		{
			waypoint = null;
			Finished = true;
			return false;
		}
	}
}
