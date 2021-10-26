using UnityEngine;

public struct Telegram
{
	//Object that sent the message
	public IMessageSender Sender;

	//Object that is to receive this message
	public IMessageReceiver Receiver;

	//Type of message to be sent
	public MessageType MessageType;

	//Messages can be dispatched immediately or delayed
	public double DispatchTime;

	//Additional Information
	public object ExtraInfo;

	public Telegram(double delay, IMessageSender sender, IMessageReceiver receiver, MessageType messageType, object extraInfo)
	{
		Sender = sender;
		Receiver = receiver;
		MessageType = messageType;
		DispatchTime = delay;
		ExtraInfo = extraInfo;
	}

}
