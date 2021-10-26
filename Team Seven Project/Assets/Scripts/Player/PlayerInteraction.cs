﻿using UnityEngine;

public partial class PlayerCharacter : MonoBehaviour
{
	[Space,Header("Interaction Mechanics")]

	[Tooltip("Range that a player can interact with objects.")]
	[SerializeField] private float _interactionRange = 2.0f;

	[Tooltip("Delay between interactions.")]
	[SerializeField] private float _interactionCooldown = 1.0f;

	[Tooltip("Layers to interact with.")]
	[SerializeField] private LayerMask _interactionLayer = 0;

	private float _interactionTimer;

	protected void UpdateInteractions()
	{
		if (InteractionKeyPressed && _interactionTimer <= 0)
		{
			_interactionTimer = _interactionCooldown;
			OnInteractionPressed();
		}
	}

	private void OnInteractionPressed()
	{
		//Get an array of interactable colliders within interaction radius
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, _interactionRange, _interactionLayer);
		foreach(Collider hitCollider in hitColliders)
		{
			if(hitCollider.TryGetComponent(out IInteractable interactable))
			{
				interactable.OnInteract(this);
			}
			
		}
	}
}

