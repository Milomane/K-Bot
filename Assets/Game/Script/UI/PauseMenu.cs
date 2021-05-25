using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    // Panel "PauseMenu"
    [SerializeField] private GameObject pauseMenuUI;
    
    // Player
    private PlayerController playerController;
    
    // Timer
    [SerializeField] private float timerSoundHoverButton = 0.5f;
    [SerializeField] private float timerValueInit;

    public void Start()
    {
        timerValueInit = timerSoundHoverButton;
        playerController = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer
        if (timerSoundHoverButton > 0f)
        {
            timerSoundHoverButton -= Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
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

    // Resume
    public void Resume()
    {
        playerController.controlCursor = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    // Pause
    void Pause()
    {
        playerController.controlCursor = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    // Options
    public void Options()
    {
        Debug.Log("Options");
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
