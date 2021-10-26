using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RecipientHandler : MonoBehaviour
{
	[SerializeField, TypeConstraint(typeof(IMessageReceiver))] 
	private List<GameObject> _recipients = new List<GameObject>();

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		foreach (GameObject obj in _recipients)
		{
			Handles.DrawAAPolyLine(transform.position, obj.transform.position+Vector3.up);
		}
	}
#endif
}
