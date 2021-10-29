using UnityEngine;

public static class MathJ 
{
	public static Vector3 CalculateProjectileVelocity(Vector3 target, Vector3 origin, float duration)
	{
		//Define the distance of x and y
		Vector3 distance = target - origin;
		Vector3 distanceXZ = distance;
		distanceXZ.y = 0f;

		//Float representation of the distance
		float height = distance.y;
		float length = distanceXZ.magnitude;

		float velocityXZ = length / duration;
		float velocityY = height / duration + 0.5f * Mathf.Abs(Physics.gravity.y) * duration;

		Vector3 result = distanceXZ.normalized * velocityXZ;
		result.y = velocityY;

		return result;
	}

	public static Vector3 CalculatePositionInTime(Vector3 velocity,Vector3 origin, float time)
	{
		Vector3 velocityXZ = velocity;
		velocityXZ.y = 0f;

		Vector3 result = origin + velocity * time;
		float velocityY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (velocity.y * time) + origin.y;

		result.y = velocityY;
		return result;
	}

	/// <summary>
	/// Remaps a value from one range to another
	/// </summary>
	/// <param name="value"></param>
	/// <param name="from1"></param>
	/// <param name="to1"></param>
	/// <param name="from2"></param>
	/// <param name="to2"></param>
	/// <returns></returns>
	public static float RemapValues(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
