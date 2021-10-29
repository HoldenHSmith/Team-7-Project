using UnityEngine;

public class SoundEmitter : MonoBehaviour, IMessageSender
{
	[SerializeField] private float _volume;
	[SerializeField] private float _distanceFalloff;


	public void EmitSound()
	{
		SendMessage();		
	}

	public void SendMessage()
	{
		SoundEmission sound = new SoundEmission(transform.position, _volume, _distanceFalloff);

		for (int i = 0; i < EnemyManager.Enemies.Count; i++)
		{
			MessageDispatcher.Instance.DispatchMessage(0, this, EnemyManager.Enemies[i], MessageType.Msg_Sound, sound);
		}
	}
}

public struct SoundEmission
{
	public Vector3 Position;
	public float Volume;
	public float DistanceFalloff;

	public SoundEmission(Vector3 position, float volume, float distanceFalloff)
	{
		Position = position;
		Volume = volume;
		DistanceFalloff = distanceFalloff;
	}
}