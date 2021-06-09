using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPowerUp : MonoBehaviour
{
    public Color hoverColor;
    public Color baseColor;
    public Color lockedColor;

    public Image background;

    public TextMeshProUGUI description;

    [SerializeField] private int idPowerUp;

    public bool canBeSelect;
    
    private bool lastCant;
    private bool locked;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!locked) 
            background.color = baseColor;
    }

    public void Select()
    {
        /*background.color = hoverColor;
        UpdateDescription();
        PlayerDeathHandler.ChangePowerUp(idPowerUp);*/
        
        if (canBeSelect && !locked)
        {
            background.color = hoverColor;
            UpdateDescription();
            PlayerDeathHandler.ChangePowerUp(idPowerUp);
            FindObjectOfType<AudioManager>().Play("ClickButton2");
        }
        // description.SetActive(true);
    }

    public void Deselect()
    {
        if (!locked)
        {
            background.color = baseColor;
        }
        // description.SetActive(false);
    }

    public void Lock()
    {
        locked = true;
        background.color = lockedColor;
    }
    public void Unlock()
    {
        locked = false;
        background.color = baseColor;
    }

    void UpdateDescription()
    {
        switch (idPowerUp)
        {
            case 0 :
                description.text = "Default";
                break;
            case 1 :
                description.text = "Explosion";
                break;
            case 2 :
                description.text = "Spring";
                break;
            case 3 :
                description.text = "Generator";
                break;
            case 4 :
                description.text = "Light";
                break;
            case 5 :
                description.text = "Accelerator";
                break;
        }
    }
}
