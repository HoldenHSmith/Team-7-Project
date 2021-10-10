using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallOnTrigger : MonoBehaviour
{
	[SerializeField] private string m_CompareTag;
	[SerializeField] private UnityEvent m_Event = null;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Event Called");
		if (m_Event != null && (other.tag == m_CompareTag))
			m_Event.Invoke();
	}
}
