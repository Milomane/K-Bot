using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juckebox : MonoBehaviour
{
    private AudioClip[] audioClips;
    private int randomSongNumber;
    private AudioClip previousSong;
    
    // Start is called before the first frame update
    void Start()
    {
        audioClips = JuckeBoxManager.instance.GetAudioClips();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButton("Interaction"))
        {
            foreach (AudioSource audioSource in JuckeBoxManager.instance.GetAudioSources())
            {
                if (audioSource.clip == previousSong)
                {
                    // Play an another song if it's the same song
                    audioSource.clip = audioSource.clip = audioClips[randomSongNumber + 1];
                }
            }
        }
    }
}
