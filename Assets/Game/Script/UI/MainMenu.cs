using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float timerSoundHoverButton = 0.5f;
    [SerializeField] private float timerValueInit;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        Debug.Log("Hover");
        if (timerSoundHoverButton <= 0f)
        {
            FindObjectOfType<AudioManager>().Play("HoverButton");
            timerSoundHoverButton = timerValueInit;
        }
    }
    
    public void PlayClickSound()
    {
        Debug.Log("Click");
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
}
