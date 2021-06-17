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
        timerValueInit = timerSoundHoverButton;
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
        SceneManager.LoadScene("HubIntro");
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
            FindObjectOfType<AudioManager>().Play("HoverButton");
            timerSoundHoverButton = timerValueInit;
        }
    }
    
    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
}
