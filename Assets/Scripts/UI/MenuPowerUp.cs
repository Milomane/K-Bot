using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPowerUp : MonoBehaviour
{
    public Color hoverColor;

    public Color baseColor;

    public Image background;

    public GameObject description;
    
    // Start is called before the first frame update
    void Start()
    {
        background.color = baseColor;
    }

    public void Select()
    {
        background.color = hoverColor;
        description.SetActive(true);
    }

    public void Deselect()
    {
        background.color = baseColor;
        description.SetActive(false);
    }
}
