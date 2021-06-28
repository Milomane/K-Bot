using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float timerSoundHoverButton = 0.5f;
    private float timerValueInit;

    void Start()
    {
        Time.timeScale = 1;
        timerValueInit = timerSoundHoverButton;
        // Set the volume 
        AudioManager.instance.audioMixerGroup.audioMixer.SetFloat("Volume", 0);
        // Play Music
        FindObjectOfType<AudioManager>().Play("MusicMainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (timerSoundHoverButton > 0f)
        {
            timerSoundHoverButton -= Time.deltaTime;
        }
    }
    
    // Play
    public void PlayGame()
    {
        //SceneManager.LoadScene("Scene_Exposition_Mecanics");
        FindObjectOfType<AudioManager>().Stop("MusicMainMenu");
        SceneManager.LoadScene("intro_cinematic");
    }
    
    // Options
    public void Options()
    {
        //Debug.Log("Option");
    }

    // Quit
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayHoverSound()
    {
        if (timerSoundHoverButton <= 0f)
        {
            FindObjectOfType<AudioManager>().Play("HoverButton2");
            timerSoundHoverButton = timerValueInit;
        }
    }
    
    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton2");
    }
}
