using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeycardDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _unlocked = false;
    [SerializeField] private AreaType _area = AreaType.Containment;
    [SerializeField] private Transform _spawnPos;

    private GameObject _interactableText;

    private Animator _animator;
    private int _openHash;

    private void Awake()
    {
        _openHash = Animator.StringToHash("Open");
        _animator = GetComponent<Animator>();

        if (!UtilsJ.FindChildByName("Interactable Text", gameObject, out _interactableText))
        {
            Debug.LogWarning($"Interactable text child prefab expected on {gameObject} ");
        }
    }

    public void OnInteract(PlayerCharacter playerCharacter)
    {
        if (CollectionManager.Instance.CheckKeyCollected(_area))
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        _unlocked = true;
        _animator.SetTrigger(_openHash);
        SaveManager.Save(_spawnPos.position);
        if (_interactableText != null)
            _interactableText.SetActive(false);
    }

    private void OnEnable()
    {
        DoorManager.RegisterDoor(this);
    }

    private void OnDisable()
    {
        DoorManager.RemoveDoor(this);
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
