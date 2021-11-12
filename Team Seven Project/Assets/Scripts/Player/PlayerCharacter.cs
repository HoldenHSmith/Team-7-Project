//Written by Jayden Hunter
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PointSampler))]
public partial class PlayerCharacter : MonoBehaviour
{

	private CharacterInput _input;                       //Reference to Input System
	private CharacterController _characterController;    //Characters controller
	private PointSampler _pointSampler;

	private void Awake()
	{
		GetRequiredComponents();
		SetupInput();
		SetupMovement();
		SetupAnimator();
		SetupPlayerThrow();

	}

	private void Start()
	{
		//transform.position = SaveManager.Instance.Current.PosToVec3();
		if (SaveManager.Instance.Current != null)
		{
			_characterController.transform.position = SaveManager.Instance.Current.GetPosition();
			_miniKeycards = SaveManager.Instance.Current.CurrentMiniKeycards;
			Debug.Log($"Keycard count: {_miniKeycards}");
		}
		//Debug.Log(transform.position);
	}

	private void Update()
	{
		UpdateInputs();
		UpdateRotation();
		UpdateAnimations();
		UpdateVelocity();
		UpdateThrow();
		UpdateInteractions();
		EndInputUpdate();
		MoveCharacter(_velocity);
		ResetInputs();
	}

	private void FixedUpdate()
	{

	}

	//Reset is called automatically by Unity when the script is first added to a gameobject or is reset 
	private void Reset()
	{
		GetRequiredComponents();
	}

	//Get the required components of the character controller
	private void GetRequiredComponents()
	{
		_characterController = GetComponent<CharacterController>();
		_pointSampler = GetComponent<PointSampler>();
	}

	public List<Transform> SamplePoints { get => _pointSampler.SamplePoints; }
}
