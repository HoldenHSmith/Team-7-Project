using UnityEngine;
using UnityEngine.Events;

public class CallOnEvent : MonoBehaviour
{
	public UnityEvent Event;

	public void CallEvent()
	{
		Event.Invoke();
	}
}
