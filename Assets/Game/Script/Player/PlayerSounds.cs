using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    // Sounds of walk (AudioClip)
    [SerializeField] private AudioClip[] walkSoundclips;

    // Sound jump
    [SerializeField] private AudioClip jumpClip;

    [SerializeField] private AudioClip landClip;

    [SerializeField] private AudioClip springClip;

    [SerializeField] private AudioClip respawnClip;

    private float defaultVolume;

    private bool isStartWalking;

    // Audio source
    private AudioSource audioSource;

    void Awake()
    {
        // Get component AudioSource
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
    }

    // Call from event animation
    public void Step()
    {
        AudioClip clip = GetRandomWalkClip();

        audioSource.PlayOneShot(clip);
    }

    public void Jump()
    {
        audioSource.PlayOneShot(jumpClip);
    }

    public void Land()
    {
        audioSource.PlayOneShot(landClip);
    }

    public void Spring()
    {
        audioSource.PlayOneShot(springClip);
    }

    public void Respawn()
    {
        audioSource.PlayOneShot(respawnClip);
    }

    // Get random sound of walk
    private AudioClip GetRandomWalkClip()
    {
        return walkSoundclips[Random.Range(0, walkSoundclips.Length)];
    }
}