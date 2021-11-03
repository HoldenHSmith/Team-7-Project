//Written by Jayden Hunter
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PointSampler))]
public partial class PlayerCharacter : MonoBehaviour
{
	public bool IsRespawning { get { return Respawning; } }

	protected CharacterInput Input;                       //Reference to Input System
	protected bool Respawning;                            //Whether the character is respawning
	protected CharacterController CharacterController;    //Characters controller
	protected PointSampler PointSampler;

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
			CharacterController.transform.position = SaveManager.Instance.Current.PosToVec3();
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
		CharacterController = GetComponent<CharacterController>();
		PointSampler = GetComponent<PointSampler>();
	}

	public List<Transform> SamplePoints { get => PointSampler.SamplePoints; }
}
