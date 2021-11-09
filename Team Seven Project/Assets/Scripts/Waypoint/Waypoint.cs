using System;
using UnityEngine;

[Serializable]
public class Waypoint
{
	public Vector3 Offset;
	public Vector3 Position;
	public float WaitTime;

	public Waypoint()
	{
		Position = new Vector3();
		WaitTime = 0f;
	}

	public Waypoint(Vector3 position) : base()
	{
		this.Position = position;
	}

	public Waypoint(Vector3 position, float waitTime)
	{
		this.Position = position;
		WaitTime = waitTime;
	}

	public void SetOffset(Vector3 parentPosition, Vector3 waypointPosition)
	{
		Offset = waypointPosition - parentPosition;
		Position = waypointPosition;
	}
}
