using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLookAt : MonoBehaviour
{
	[Tooltip("Target to follow")]
	public GameObject target;

	[Tooltip("How far offset the camera is from the Target")]
	public Vector3 offSet;

	private void Update()
	{
		if(target != null)
		{
			transform.position = target.transform.position + offSet;
		}
	}
}
