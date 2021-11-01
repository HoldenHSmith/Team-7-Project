using UnityEngine;

public class EnemySettings : MonoBehaviour
{
	[Tooltip("The speed in which the enemy will walk along it's path.")]
	public float WalkSpeed;
	public float WalkAcceleration;
	public float WalkTurnSpeed = 360;

	[Tooltip("The speed in which the enemy will go an investigate a sound.")]
	public float WalkInspectSpeed;
	public float InspectAcceleration;
	public float InspectTurnSpeed = 540;

	[Tooltip("The speed in which the enemy will run.")]
	public float RunSpeed;
	public float RunAcceleration;
	public float RunTurnSpeed = 540;

	[Tooltip("The speed in which the enemy will walk when the camera has alerted them to the player's position.")]
	public float WalkCameraAlertSpeed;
	public float WalkCameraAlertAcceleration;
	public float WalkCameraAlertTurnSpeed = 420;

	[Tooltip("Distance the Enemy can spot the player.")]
	public float FollowPlayerDistance = 10f;

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

	[Tooltip("The range in which the guard will automatically detect the player.")]
	public float AutoDetectRange = 1;

	[Tooltip("The range in which the guard will automatically catch the player.")]
	public float AutoCatchRange = 0.25f;
}
