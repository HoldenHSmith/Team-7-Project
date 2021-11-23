using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, ExecuteInEditMode]
public class WaypointManager : MonoBehaviour
{
	public LoopType Type;
	public bool Finished = false;
	public List<Waypoint> Waypoints;

	private bool _forward = true;
	private int _currentIndex = 0;
	private List<Waypoint> m_PreviousWaypoints;
	private Vector3 _prevPosition;

	private void OnEnable()
	{
		Initialize();
	}

	private void Reset()
	{
		Initialize();
	}


	private void Initialize()
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
				case LoopType.PingPong:
					return GetNextWaypointPingPong(out waypoint);
				case LoopType.Repeat:
					return GetWaypointLoop(out waypoint);
				case LoopType.Once:
					return GetWaypointOnce(out waypoint);

				default:
					waypoint = null;
					return false;
			}
		}
	}

	private void CheckTargetMoved()
	{
		if (transform.position != _prevPosition)
		{
			if (Waypoints != null && Waypoints.Count > 0)
			{
				for (int i = 0; i < Waypoints.Count; i++)
				{
					Waypoint waypoint = Waypoints[i];

					waypoint.Position = transform.position + waypoint.Offset;
				}
			}
		}

		_prevPosition = transform.position;
	}

	private bool CheckUpdateWaypoints(int index)
	{
		if (Waypoints.Count != m_PreviousWaypoints.Count ||
			Waypoints[index].Position != m_PreviousWaypoints[index].Position ||
			Waypoints[index].WaitTime != m_PreviousWaypoints[index].WaitTime)
		{
			UpdateWaypoints();
			return true;
		}
		return false;
	}

	private void UpdateWaypoints()
	{
		if (Waypoints != null)
		{
			for (int i = 0; i < Waypoints.Count; i++)
			{
				Waypoints[i].Position.y = 0.1f;
			}
			m_PreviousWaypoints = Waypoints.Select(wp => new Waypoint(wp.Position, wp.WaitTime)).ToList();
		}

	}

	private bool GetNextWaypointPingPong(out Waypoint waypoint)
	{
		if (_forward)
		{
			if (_currentIndex < Waypoints.Count)
			{
				waypoint = Waypoints[_currentIndex];
				_currentIndex++;
				return true;
			}
			else
			{
				_forward = false;
				_currentIndex--;
				waypoint = Waypoints[_currentIndex];
				return true;
			}
		}
		else
		{
			if (_currentIndex >= 0)
			{
				waypoint = Waypoints[_currentIndex];
				_currentIndex--;
				return true;
			}
			else
			{
				_forward = true;
				_currentIndex++;
				waypoint = Waypoints[_currentIndex];
				return true;
			}
		}
	}

	private bool GetWaypointLoop(out Waypoint waypoint)
	{
		if (_currentIndex < Waypoints.Count)
		{
			waypoint = Waypoints[_currentIndex];
			_currentIndex++;
			return true;
		}
		else
		{
			_currentIndex = 0;
			waypoint = Waypoints[_currentIndex];
			_currentIndex++;
			return true;
		}
	}

	private bool GetWaypointOnce(out Waypoint waypoint)
	{
		if (_currentIndex < Waypoints.Count)
		{
			waypoint = Waypoints[_currentIndex];
			_currentIndex++;
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
