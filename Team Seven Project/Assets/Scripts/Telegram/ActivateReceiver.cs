using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateReceiver : MonoBehaviour, IMessageReceiver
{
	[SerializeField] private List<UnityEvent> _events = new List<UnityEvent>();

	public bool ReceiveMessage(Telegram message)
	{
		if (message.MessageType == MessageType.Msg_Activate)
		{
			for (int i = 0; i < _events.Count; i++)
			{
				_events[i].Invoke();
			}
			return true;
		}

		return false;
	}
}
