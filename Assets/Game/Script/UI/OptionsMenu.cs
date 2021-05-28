using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    // List of the resolutions in options menu
    public TMP_Dropdown resolutionDropdown;

    // Displays of resolutions
    private Resolution[] resolutions;

    // Timer
    [SerializeField] private float timerSoundHoverButton = 0.5f;
    private float timerValueInit;

    // Panel "PauseMenu"
    [SerializeField] private GameObject pauseMenuUI;

    // Index
    private int currentResolutionIndex;

    void Start()
    {
        timerValueInit = timerSoundHoverButton;

        // Resolutions
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60 || resolution.refreshRate == 144).ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        currentResolutionIndex = 0;

        // Loop to change Resolution in string
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            // Avoid duplicates in the options
            /*if (!options.Contains(option))
            {
                options.Add(option);
            }*/
            // options = options.Distinct().ToList(); -> work but fucked up any way

            // Update the current resolution
            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Avoid duplicates in the options
        resolutionDropdown.AddOptions(options);
        // Change the value with the current resolution
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {
        // Timer 
        if (timerSoundHoverButton > 0f)
        {
            timerSoundHoverButton -= Time.deltaTime;
        }

        if (pauseMenuUI != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenuUI.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    // Volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 40);
    }

    //Resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Graphics
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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

    public void BackButton()
    {
        FindObjectOfType<AudioManager>().PlayOneShot("ClickButton");
    }
}