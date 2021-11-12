using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPerspective : MonoBehaviour
{
	private Vector3 _eulerAngles = new Vector3(0, 0, 0);

	private void Awake()
	{
		transform.rotation = Quaternion.Euler(_eulerAngles);
	}
}
