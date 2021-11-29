using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space, Header("Interaction Mechanics")]

	[Tooltip("Range that a player can interact with objects.")]
	[SerializeField] private float _interactionRange = 2.0f;

	[Tooltip("Delay between interactions.")]
	[SerializeField] private float _interactionCooldown = 1.0f;

	[Tooltip("Layers to interact with.")]
	[SerializeField] private LayerMask _interactionLayer = 0;

	[Tooltip("Layers that block interactions.")]
	[SerializeField] private LayerMask _interactionBlockLayer = 0;

	private int _miniKeycards = 0;

	private float _interactionTimer;

	protected void UpdateInteractions()
	{
		_interactionTimer -= Time.deltaTime;

		if (_interactKeyReleasedThisFrame && _interactionTimer <= 0)
		{
			_interactionTimer = _interactionCooldown;
			OnInteractionPressed();
		}
	}

	private void OnInteractionPressed()
	{

		//Get an array of interactable colliders within interaction radius
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, _interactionRange, _interactionLayer);
		foreach (Collider hitCollider in hitColliders)
		{
			if (hitCollider.TryGetComponent(out IInteractable interactable))
			{
				Vector3 direction = hitCollider.transform.position - transform.position;
				float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
				RaycastHit hit;
				//Raycast
				if (!Physics.Raycast(transform.position + Vector3.up, direction, out hit, distance - 1, _interactionBlockLayer, QueryTriggerInteraction.Ignore) || hit.collider.tag == "Door")
				{
					if (hitCollider.tag == "Beaker")
					{
						if (_hasBeaker || _throwDisabled)
							return;
					}

					if (interactable.OnInteract(this))
					{
						Debug.Log($"Interact Collider: {hitCollider.gameObject.name}");
						if (hitCollider.tag == "Door")
						{
							_animator.Play("Swipe");
							BlockInputForTime(_doorInteractBlockTime);
							_playerSound.PlayKeycardUsed();
						}
						else
						{
							if (hitCollider.tag != "Note")
							{
								BlockInputForTime(_keycardInteractBlockTime);
								_animator.Play("Collect");
								_playerSound.PlayInteract();
							}
						}
						break;
					}
				}

			}

		}
	}

	public int MiniKeycards { get => _miniKeycards; set => _miniKeycards = value; }
}

