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

    public void Start()
    {
        playerController = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
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
}
