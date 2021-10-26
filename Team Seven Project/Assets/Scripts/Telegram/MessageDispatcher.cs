using System;
using System.Collections.Generic;

public enum MessageType
{
	Msg_PlayerSpotted,
	Msg_Reset
}

public sealed class MessageDispatcher
{
	private static readonly MessageDispatcher _instance = new MessageDispatcher();
	SortedSet<Telegram> _messageQueue = new SortedSet<Telegram>(new SortTelegramDispatchTime());

	public void DispatchMessage(double delay, IMessageSender sender, IMessageReceiver receiver, MessageType messageType, object extraInfo)
	{
		// Create the telegram.
		Telegram telegram = new Telegram(delay, sender, receiver, messageType, extraInfo);
		
		// If there is no delay, route the telegram immediately.
		if(delay <= 0.0)
		{
			Discharge(receiver, telegram);
		}
		else
		{
			double currentTime = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds + delay;
			telegram.DispatchTime = currentTime + delay;

			_messageQueue.Add(telegram);
		}
	}

	public void DispatchDelayedMessages()
	{
		// Get current time.
		double currentTime = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;

		// Peek at the message queue to see if any telegrams need dispatching.
		// Remove all telegrams from the front of the queue that have gone
		// past their sell-by date.
		while( (_messageQueue.Min.DispatchTime < currentTime) && (_messageQueue.Min.DispatchTime > 0))
		{
			// Read the telegram from the front of the queue.
			Telegram telegram = _messageQueue.Min;

			// Get recipient.
			IMessageReceiver receiver = telegram.Receiver;

			// Send the telegram to the recipient.
			Discharge(receiver, telegram);

			// Remove it from the queue
			_messageQueue.Remove(_messageQueue.Min);
		}
	}

	private void Discharge(IMessageReceiver receiver, Telegram telegram)
	{
		receiver.ReceiveMessage(telegram);
	}

	public static MessageDispatcher Instance
	{
		get
		{
			return _instance;
		}
	}
}
