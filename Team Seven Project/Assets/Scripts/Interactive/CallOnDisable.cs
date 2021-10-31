using UnityEngine;
using UnityEngine.Events;

public class CallOnDisable : MonoBehaviour
{
	[SerializeField] UnityEvent _event;

	private void OnDisable()
	{
		_event.Invoke();
	}
}
