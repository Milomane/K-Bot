using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuckeBoxManager : MonoBehaviour
{
    public static JuckeBoxManager instance;

    [SerializeField] private AudioClip[] audioClips;

    [SerializeField] private AudioSource[] audioSources;

    // random number for random first song
    private int numberSong;

    void Awake()
    {
        // Implementation of the Saint Gleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numberSong = Random.Range(0, audioClips.Length);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            // If there is no song
            if (!audioSource.isPlaying)
            {
                ChangeMusic(audioSource);

                // Play
                audioSource.Play();
            }
        }
    }

    private void CheckNumberSong()
    {
        // Avoid IndexOutOfRangeException:
        if (numberSong >= audioClips.Length)
        {
            numberSong = 0;
        }
    }

    public void ChangeMusic(AudioSource audioSource)
    {
        // next number song
        numberSong++;
        
        // CheckNumberSong -> Avoid IndexOutOfRangeException:
        CheckNumberSong();

        // Update the clip with the current song
        audioSource.clip = audioClips[numberSong]; // = currentSong
    }

    public AudioClip[] GetAudioClips()
    {
        return audioClips;
    }

    public AudioSource[] GetAudioSources()
    {
        return audioSources;
    }
}