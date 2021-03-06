using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool isMenuPauseClosed = true;

    // Panel "PauseMenu"
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsMenuUI;
    
    // Player
    private PlayerController playerController;
    
    // Timer
    [SerializeField] private float timerSoundHoverButton = 0.5f;
    [SerializeField] private bool canPlaySound = true;

    public void Start()
    {
        playerController = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMenuPauseClosed)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !optionsMenuUI.activeInHierarchy)
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    // Resume
    public void Resume()
    {
        playerController.controlCursor = false;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Time.timeScale = 1f;
        gameIsPaused = false;
        isMenuPauseClosed = true;
    }

    // Pause
    void Pause()
    {
        playerController.controlCursor = true;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        Time.timeScale = 0f;
        gameIsPaused = true;
        isMenuPauseClosed = false;
    }

    // Options
    public void Options()
    {
        // Debug.Log("Options");
    }

    // Main menu
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitForSound()
    {
        canPlaySound = false;
        yield return new WaitForSecondsRealtime(timerSoundHoverButton);
        canPlaySound = true;
    }
    
    public void PlayHoverSound()
    {
        if (canPlaySound)
        {
            StartCoroutine(WaitForSound());
            FindObjectOfType<AudioManager>().Play("HoverButton2");
        }
    }
    
    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton2");
    }
}
