using System;
using UnityEngine;

[Serializable]
public class Waypoint
{
	public Vector3 Position;
	public bool HasWaitTime;
	public float WaitTime;

	public Waypoint()
	{
		Position = new Vector3();
		HasWaitTime = false;
		WaitTime = 0f;
	}

	public Waypoint(Vector3 position) : base()
	{
		this.Position = position;
	}

	public Waypoint(Vector3 position, bool hasWaitTime, float waitTime)
	{
		this.Position = position;
		HasWaitTime = hasWaitTime;
		WaitTime = waitTime;
	}
}
