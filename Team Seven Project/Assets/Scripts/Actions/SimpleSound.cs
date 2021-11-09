using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSound : MonoBehaviour
{
    public List<AudioClip> SoundClips;
    private CharacterInput _input;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (SoundClips == null)
            SoundClips = new List<AudioClip>();

        _audioSource = GetComponent<AudioSource>();
    }
}
