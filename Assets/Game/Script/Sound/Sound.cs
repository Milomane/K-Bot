using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // Name
    public string name;
    // Audio clip
    public AudioClip clip;
    // Volume of the sound
    [Range(0f, 1f)]
    public float volume;
    // Pitch of the sound
    [Range(.1f, 3f)]
    public float pitch;
    // Loop
    public bool isLoop;
    // Audio source
    [HideInInspector]
    public AudioSource source;

}
