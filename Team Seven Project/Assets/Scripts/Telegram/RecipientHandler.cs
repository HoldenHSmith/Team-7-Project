using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#endif

public class RecipientHandler : MonoBehaviour
{
	[SerializeField, TypeConstraint(typeof(IMessageReceiver))]
	private List<GameObject> _recipients = new List<GameObject>();

	private List<IMessageReceiver> _recipientInterfaceList = new List<IMessageReceiver>();

	private void Awake()
	{
		for (int i = 0; i < _recipients.Count; i++)
		{
			IMessageReceiver recipient = _recipients[i].GetComponent<IMessageReceiver>();
			_recipientInterfaceList.Add(recipient);
		}
	}

	public List<IMessageReceiver> Recipients { get => _recipientInterfaceList; }

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		foreach (GameObject obj in _recipients)
		{
			if (obj != null)
				GizmosJ.DrawDirectionalLine(transform.position, obj.transform.position, Color.green);
		}
	}
#endif
}
