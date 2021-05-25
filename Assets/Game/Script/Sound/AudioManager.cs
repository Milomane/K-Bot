using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup audioMixerGroup;
    // Saint Gleton
    public static AudioManager instance;
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
        
        // Update the source sound
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.outputAudioMixerGroup = audioMixerGroup;
            sound.source.loop = sound.isLoop;
        }
    }

    // Find a sound
    private Sound GetSoundFromAudioManager(string name) 
    { 
        // Find a sound with the name 
        Sound s = Array.Find(sounds, sound => sound.name == name); // Lambda expression ( ... => ...) 
        return s; 
    } 
 
    // Play a sound
    public void Play(string name) 
    { 
        Sound s = GetSoundFromAudioManager(name);
        s.source.Play(); 
    }

    public void PlayOneShot(string name)
    {
        Sound s = GetSoundFromAudioManager(name);
        s.source.PlayOneShot(s.clip); // TODO : trouver le bon code pour PlayOneShot
    }
 
    // Stop a sound
    public void Stop(string name) 
    { 
        Sound s = GetSoundFromAudioManager(name);
        s.source.Stop(); 
    } 
}
