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
                description.text = "1";
                break;
            case 2 :
                description.text = "2";
                break;
            case 3 :
                description.text = "3";
                break;
            case 4 :
                description.text = "4";
                break;
            case 5 :
                description.text = "5";
                break;
            case 6 :
                description.text = "6";
                break;
        }
    }
}
