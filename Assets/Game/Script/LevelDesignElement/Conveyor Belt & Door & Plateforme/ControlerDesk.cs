using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerDesk : MonoBehaviour
{
    public GameObject imagePressCanvas;
    private bool ActivationOn;
    
    public List<GameObject> objectsList;
    
    // Start is called before the first frame update
    void Start()
    {
        imagePressCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ActivationOn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("press E");
                for (int i = 0; i <= objectsList.Count -1; i++)
                {
                    if (objectsList[i].GetComponent<ConveyorBeltModule>() == true)
                    {
                        if (objectsList[i].GetComponent<ConveyorBeltModule>().conveyorActivation == true)
                        {
                            objectsList[i].GetComponent<ConveyorBeltModule>().DesactivationBoard();
                        }
                        else
                        {
                            objectsList[i].GetComponent<ConveyorBeltModule>().ActivationBoard();
                        }
                    }
                    else if (objectsList[i].GetComponent<DoorModule>() == true)
                    {
                        if (objectsList[i].GetComponent<DoorModule>().doorActivation == true)
                        {
                            objectsList[i].GetComponent<DoorModule>().DesactivationBoard();
                        }
                        else
                        {
                            objectsList[i].GetComponent<DoorModule>().ActivationBoard();
                        }
                    }
                    else if (objectsList[i].GetComponent<PlateformeModule>() == true)
                    {
                        if (objectsList[i].GetComponent<PlateformeModule>().plateformeActivation == true)
                        {
                            objectsList[i].GetComponent<PlateformeModule>().DesactivationBoard();
                        }
                        else
                        {
                            objectsList[i].GetComponent<PlateformeModule>().ActivationBoard();
                        }
                    }
                    else
                    {
                        Debug.Log("error module");
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            imagePressCanvas.SetActive(true);
            ActivationOn = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            imagePressCanvas.SetActive(false);
            ActivationOn = false;
        }
    }
}
