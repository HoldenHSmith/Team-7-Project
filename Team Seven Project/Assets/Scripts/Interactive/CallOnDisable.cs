using UnityEngine;
using UnityEngine.Events;

public class CallOnDisable : MonoBehaviour
{
	[SerializeField] UnityEvent _event = null;

	private void OnDisable()
	{
		if (_event != null)
			_event.Invoke();
	}
}
