using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLookAt : MonoBehaviour
{
	[Tooltip("Target to follow")]
	public GameObject Target;

	[Tooltip("How far offset the camera is from the Target")]
	public Vector3 Offset;

	private void LateUpdate()
	{
		if(Target != null)
		{
			transform.position = Target.transform.position + Offset;
		}
	}
}
