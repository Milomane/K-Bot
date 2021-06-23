using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuckeBoxManager : MonoBehaviour
{
    public static JuckeBoxManager instance;
    
    [SerializeField] private AudioClip[] audioClips;
    
    [SerializeField] private AudioSource[] audioSources;
    
    // random number for random song
    private int randomSongNumber;
    
    // Previous song
    private AudioClip previousSong;

    private bool canGetRandomNumber;

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
        
        // Keep the Audio Manager if we change the scene
        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canGetRandomNumber)
        {
            // Get a random int
            randomSongNumber = Random.Range(0, audioClips.Length);
            canGetRandomNumber = false;
        }
        
        foreach (AudioSource audioSource in audioSources)
        {
            // If there is no song
            if (!audioSource.isPlaying)
            {
                // Play a random song
                audioSource.clip = audioClips[randomSongNumber];
            
                /*if (audioSource.clip == previousSong)
                {
                    // Play an another song if it's the same song
                    audioSource.clip = audioSource.clip = audioClips[randomSongNumber + 1];
                }
            
                // Update previousSong valor
                previousSong = audioSource.clip;*/
            
                // Play
                audioSource.Play();
            }
            else
            {
                canGetRandomNumber = true;
            }
        }
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
