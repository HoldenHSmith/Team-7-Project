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
			if (obj != null)
				GizmosJ.DrawDirectionalLine(transform.position, obj.transform.position, Color.red);
		}
	}
#endif
}
