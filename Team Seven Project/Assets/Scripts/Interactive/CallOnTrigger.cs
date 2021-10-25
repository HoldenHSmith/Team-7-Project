using UnityEngine;
using UnityEngine.Events;

public class CallOnTrigger : MonoBehaviour
{
	[SerializeField] private string _compareTag;
	[SerializeField] private UnityEvent _event = null;

	private void OnTriggerEnter(Collider other)
	{
		if (_event != null && (other.tag == _compareTag))
			_event.Invoke();
	}
}
