using UnityEngine;

public class EnemySettings : MonoBehaviour
{
	[Tooltip("The speed in which the enemy will walk along it's path.")]
	public float WalkSpeed;

	[Tooltip("The speed in which the enemy will go an investigate a sound.")]
	public float WalkInspectSpeed;

	[Tooltip("The speed in which the enemy will walk when the camera has alerted them to the player's position.")]
	public float WalkCameraAlertSpeed;

	[Tooltip("Used to determine what minimum distance is required until the player is 'at' the waypoint.")]
	public float DistanceToWaypointSatisfaction = 0.1f;

	[Tooltip("Distance from last known Player position to satisfy Camera Detected State.")]
	public float DistanceToLastKnownPlayerPosition = 0.5f;

	[Tooltip("Dictates how long the guard will search for the player before resuming their patrol.")]
	public float InvestigationTime;

	[Tooltip("Radius in which the guard will search for the Player.")]
	public float InvestigationRadius = 5;

	[Tooltip("Time until it randomizes another investigation location within radius.")]
	public float InvestigationDelay = 1;

	[Tooltip("Angle of the view Cone in which the guard can spot the player.")]
	public float ViewConeAngle = 25;
}
