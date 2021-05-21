using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        FindObjectOfType<AudioManager>().Play("HoverButton");
    }
    
    public void PlayClickSound()
    {
        Debug.Log("Click");
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
}
