using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersWheel : MonoBehaviour
{
    public Vector2 normalisedMousePosition;
    public float currentAngle;
    public int selection;
    private int previousSelection;

    public GameObject[] menuItems;

    private MenuPowerUp menuPowerUp;
    private MenuPowerUp previousMenuPowerUp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse Position
        normalisedMousePosition = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
        // Current angle (in radian)
        currentAngle = Mathf.Atan2(normalisedMousePosition.y, normalisedMousePosition.x) * Mathf.Rad2Deg - 30;
        
        currentAngle = (currentAngle + 360) % 360;
        // Selection (number)
        selection = (int)currentAngle / 60;

        if (selection != previousSelection)
        {
            previousMenuPowerUp = menuItems[previousSelection].GetComponent<MenuPowerUp>();
            previousMenuPowerUp.Deselect();
            previousSelection = selection;

            menuPowerUp = menuItems[selection].GetComponent<MenuPowerUp>();
            menuPowerUp.Select();
        }
        
        Debug.Log(currentAngle);
    }
}
