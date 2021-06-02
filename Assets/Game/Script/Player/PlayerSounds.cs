using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    // Sounds of walk (AudioClip)
    [SerializeField]
    private AudioClip[] walkSoundclips;

    // Audio source
    private AudioSource audioSource;

    void Awake()
    {
        // Get component AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Call from event animation
    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    // Get random sound of walk
    private AudioClip GetRandomClip()
    {
        return walkSoundclips[Random.Range(0, walkSoundclips.Length)];
    }
}
