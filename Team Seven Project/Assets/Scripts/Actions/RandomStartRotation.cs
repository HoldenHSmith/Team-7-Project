using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartRotation : MonoBehaviour
{
	[SerializeField] private Vector3 minRotation = Vector3.zero;
	[SerializeField] private Vector3 maxRotation = Vector3.zero;

	private void Awake()
	{
		Vector3 rotation = Vector3.zero;
		rotation.x = Random.Range(minRotation.x, maxRotation.x);
		rotation.y = Random.Range(minRotation.y, maxRotation.y);
		rotation.z = Random.Range(minRotation.z, maxRotation.z);
		transform.eulerAngles = rotation;
	}

}
