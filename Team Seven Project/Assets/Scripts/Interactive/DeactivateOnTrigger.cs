using UnityEngine;

public class DeactivateOnTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		this.gameObject.SetActive(false);
	}
}
