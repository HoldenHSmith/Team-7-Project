using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckForAudio : MonoBehaviour
{
	private AudioSource[] _sources;

	private void Start()
	{
		//Get all audio sources in the scene.
		_sources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
	}

}
