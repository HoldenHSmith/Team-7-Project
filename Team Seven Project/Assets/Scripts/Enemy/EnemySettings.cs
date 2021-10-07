using UnityEngine;

public class EnemySettings : MonoBehaviour
{
	[Tooltip("The speed in which the enemy will walk along it's path.")]
	public float WalkSpeed;

	[Tooltip("The speed in which the enemy will go an investigate a sound.")]
	public float WalkInspectSpeed;

	[Tooltip("Used to determine what minimum distance is required until the player is 'at' the waypoint.")]
	public float DistanceToWaypointSatisfaction = 0.1f;
}
