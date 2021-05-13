using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPowerUp : MonoBehaviour
{
    public Color hoverColor;

    public Color baseColor;

    public Image background;

    public Text description;

    [SerializeField] private int idPowerUp;
    
    // Start is called before the first frame update
    void Start()
    {
        background.color = baseColor;
    }

    public void Select()
    {
        background.color = hoverColor;
        UpdateDescription();
        PlayerDeathHandler.ChangePowerUp(idPowerUp - 1);
        // description.SetActive(true);
    }

    public void Deselect()
    {
        background.color = baseColor;
        // description.SetActive(false);
    }

    void UpdateDescription()
    {
        switch (idPowerUp)
        {
            case 1 :
                description.text = "Normal";
                break;
            case 2 :
                description.text = "Explosion";
                break;
            case 3 :
                description.text = "Spring";
                break;
            case 4 :
                description.text = "Generator";
                break;
            case 5 :
                description.text = "Lamp";
                break;
            case 6 :
                description.text = "Glue";
                break;
        }
    }
}
