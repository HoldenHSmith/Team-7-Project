using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour, IMessageSender
{
	private EnemyManager _enemyManager;
	private MessageDispatcher _messageDispatcher;

	public void SendMessage()
	{
		_messageDispatcher.DispatchMessage(0, this, _enemyManager.Enemies[0], MessageType.Msg_PlayerSpotted, new Vector3(-5,23,5));
	}

	private void Awake()
	{
		_enemyManager = GetComponentInChildren<EnemyManager>();
		_messageDispatcher = MessageDispatcher.Instance;
	}

	private void Update()
	{
		if(Keyboard.current.rightBracketKey.wasPressedThisFrame)
		{
			SendMessage();
		}
	}
}
