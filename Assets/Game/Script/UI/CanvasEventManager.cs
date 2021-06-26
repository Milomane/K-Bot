using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasEventManager : MonoBehaviour
{
    public static CanvasEventManager instance;
    
    [Header("Selector")]
    public bool selectorActive;
    [SerializeField] private GameObject selectorObject;
    [SerializeField] private Animator selectorAnimator;
    [SerializeField] private PowersWheel powerWheel;
    private bool selectorIsClosing;
    
    [Header("InGameUi")] 
    public bool inGameUiActive = true;
    [SerializeField] private GameObject inGameUiObject;
    [SerializeField] private TextMeshProUGUI powerUpText;
    public PlayerDeathHandler.DeathType deathTypeSelected = PlayerDeathHandler.DeathType.crunshed;
    
    [SerializeField] private Image[] kbotHeads;
    [SerializeField] private Sprite kbotHead;

    public LookPlayer dialogueLookPlayer;
    public TMP_Text npcText;

    public Transform accBarPivot;
    public GameObject accBarObject;



    void Start()
    {
        instance = this;
    }

    void Update()
    {
        UpdateSelectorUi();
        UpdateInGameUi();
        for (int i = 0; i < kbotHeads.Length; i++)
        {
            if (i < PlayerDeathHandler.instance.GetNbBodyAvailable())
            {
                if (!kbotHeads[i].IsActive())
                {
                    kbotHeads[i].gameObject.SetActive(true);
                }
                kbotHeads[i].sprite = kbotHead;
            }
            else
            {
                kbotHeads[i].gameObject.SetActive(false);
            }
        }
    }

    void UpdateSelectorUi()
    {
        // Manage power up selector activation an animation
        if (selectorActive && !selectorObject.activeInHierarchy && PlayerDeathHandler.instance.initPowerUp)
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
                    powerUpText.text = "Accelerator";
                    break;
                case PlayerDeathHandler.DeathType.crunshed:
                    powerUpText.text = "None";
                    break;
                default :
                    Debug.LogError("Error in UpdateInGameUi, wrong value for deathTypeSelected");
                    return;
            }
        }
    }

    public void UpdateUnlockedDeath(List<PlayerDeathHandler.DeathType> unlockedDeath)
    {
        for (int i = 0; i < 6; i++)
        {
            bool unlocked = false;
            foreach (var death in unlockedDeath)
            {
                if ((int) death == i)
                    unlocked = true;
            }

            selectorObject.SetActive(true);
            
            if (unlocked)
                powerWheel.menuItemsLogicOrder[i].GetComponent<MenuPowerUp>().Unlock();
            else
                powerWheel.menuItemsLogicOrder[i].GetComponent<MenuPowerUp>().Lock();
            
            if (!selectorActive)
                selectorObject.SetActive(false);
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
