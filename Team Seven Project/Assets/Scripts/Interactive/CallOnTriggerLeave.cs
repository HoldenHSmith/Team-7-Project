using UnityEngine;
using UnityEngine.Events;

public class CallOnTriggerLeave : MonoBehaviour
{
	[SerializeField] private string _compareTag = "";
	[SerializeField] private UnityEvent _event = null;

	private void OnTriggerExit(Collider other)
	{
		if (_event != null && (other.tag == _compareTag))
			_event.Invoke();
	}
}
