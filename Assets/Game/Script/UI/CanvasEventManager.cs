using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasEventManager : MonoBehaviour
{
    public static CanvasEventManager instance;
    
    [Header("Selector")]
    public bool selectorActive;
    [SerializeField] private GameObject selectorObject;
    [SerializeField] private Animator selectorAnimator;
    private bool selectorIsClosing;

    [Header("InGameUi")] 
    public bool inGameUiActive = true;
    [SerializeField] private GameObject inGameUiObject;
    [SerializeField] private TextMeshProUGUI powerUpText;
    public PlayerDeathHandler.DeathType deathTypeSelected = PlayerDeathHandler.DeathType.normal;
    



    void Start()
    {
        instance = this;
    }

    void Update()
    {
        UpdateSelectorUi();
        UpdateInGameUi();
    }

    void UpdateSelectorUi()
    {
        // Manage power up selector activation an animation
        if (selectorActive && !selectorObject.activeInHierarchy)
        {
            selectorObject.SetActive(true);
            selectorAnimator.Play("OpenPowerUpSelector");
        } 
        else if (!selectorActive && selectorObject.activeInHierarchy && !selectorIsClosing)
        {
            StartCoroutine(CloseSelector());
        }
    }

    void UpdateInGameUi()
    {
        inGameUiObject.SetActive(inGameUiActive);
        if (inGameUiActive)
        {
            switch (deathTypeSelected)
            {
                case PlayerDeathHandler.DeathType.normal:
                    powerUpText.text = "Normal";
                    break;
                case PlayerDeathHandler.DeathType.explosion:
                    powerUpText.text = "Explosion";
                    break;
                case PlayerDeathHandler.DeathType.spring:
                    powerUpText.text = "Spring";
                    break;
                case PlayerDeathHandler.DeathType.generator:
                    powerUpText.text = "Generator";
                    break;
                case PlayerDeathHandler.DeathType.lamp:
                    powerUpText.text = "Lamp";
                    break;
                case PlayerDeathHandler.DeathType.accelerator:
                    powerUpText.text = "Glue";
                    break;
                default :
                    Debug.LogError("Error in UpdateInGameUi, wrong value for deathTypeSelected");
                    return;
            }
        }
    }

    IEnumerator CloseSelector()
    {
        selectorIsClosing = true;
        selectorAnimator.Play("ClosePowerUpSelector");
        yield return new WaitForSeconds(selectorAnimator.GetCurrentAnimatorStateInfo(0).length);
        selectorObject.SetActive(false);
        selectorIsClosing = false;
    }
}
