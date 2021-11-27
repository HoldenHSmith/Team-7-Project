using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeycardDoor : MonoBehaviour, IInteractable
{
	[SerializeField] private bool _unlocked = false;
	[SerializeField] private AreaType _area = AreaType.Containment;
	[SerializeField] private Transform _spawnPos = null;

	private GameObject _interactableText;

	private Animator _animator;
	private int _openHash;
	private AudioSource _audioSource;
	private bool _activated = false;
	[SerializeField] private float _delay = 0.5f;
	private void Awake()
	{
		_openHash = Animator.StringToHash("Open");
		_animator = GetComponent<Animator>();

		if (!UtilsJ.FindChildByName("Interactable Text", gameObject, out _interactableText))
		{
			Debug.LogWarning($"Interactable text child prefab expected on {gameObject} ");
		}
		_audioSource = GetComponent<AudioSource>();
	}

	public bool OnInteract(PlayerCharacter playerCharacter)
	{
		if (GameManager.Instance.CollectionManager.CheckKeyCollected(_area))
		{
			Unlock();
			return true;
		}
		return false;
	}
	private void Update()
	{
		if (_activated)
		{
			if (_delay > 0)
				_delay -= Time.deltaTime;
			else if (_animator != null)
			{
				_animator.SetTrigger(_openHash);
				_audioSource.Play();

				_activated = false;
			}
			if (_interactableText != null)
				_interactableText.SetActive(false);
		}
	}
	public void Unlock()
	{
		if (!_unlocked)
		{
			_unlocked = true;
			_activated = true;
			SaveManager.Save(_spawnPos.position, transform.rotation);
			
		}
	}

	private void OnEnable()
	{
		//DoorManager.RegisterDoor(this);
	}

	private void OnDisable()
	{
		//DoorManager.RemoveDoor(this);
	}

	public void SetUnlocked(bool unlocked)
	{
		_unlocked = unlocked;
		if (_unlocked && _animator != null)
			_animator.Play("Is_Open");
	}

	public bool Unlocked { get => _unlocked; }
	public Vector3 SpawnPos { get => _spawnPos.position; }
}
