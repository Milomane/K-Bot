using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerDesk : MonoBehaviour
{
    public GameObject imagePressCanvas;
    private bool ActivationOn;
    
    public List<GameObject> conveyorList;
    
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
                for (int i = 0; i <= conveyorList.Count -1; i++)
                {
                    if (conveyorList[i].GetComponent<ConveyorBeltModule>().conveyorActivation == true)
                    {
                        conveyorList[i].GetComponent<ConveyorBeltModule>().DesactivationBoard();
                    }
                    else
                    {
                        conveyorList[i].GetComponent<ConveyorBeltModule>().ActivationBoard();
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
