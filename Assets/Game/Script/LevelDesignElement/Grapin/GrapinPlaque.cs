using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapinPlaque : MonoBehaviour
{
    public bool grappin, move;

    public GrapinModule module;
    public GameObject imagePressCanvas, player;
    private bool ActivationOn;

    public void Start()
    {
        if (move)
        {
            imagePressCanvas.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (move && ActivationOn)
        {
            if (player.gameObject.CompareTag("Player"))
            {
                if (Input.GetButtonDown("Interaction"))
                {
                    if (!player.gameObject.GetComponent<PlayerController>().stopMovement)
                    {
                        player.gameObject.GetComponent<PlayerController>().stopMovement = true;
                        Debug.Log("press e pour true");
                    }
                    else if(player.gameObject.GetComponent<PlayerController>().stopMovement)
                    {
                        player.gameObject.GetComponent<PlayerController>().stopMovement = false;
                        Debug.Log("press e pour false");
                    }
                    else
                    {
                        Debug.Log("flute c'est un bug la");
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (module != null)
        {
            if (grappin)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.Down();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (module != null)
        {
            if (grappin)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.ObjectRecup();
                    module.verouillage = false;
                }
            }
            else if (move)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    imagePressCanvas.SetActive(false);
                    ActivationOn = false;
                    player = null;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (module != null)
        {
            if (grappin)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.DropObject();
                    module.verouillage = true;
                }
            }
            else if (move)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    imagePressCanvas.SetActive(true);
                    ActivationOn = true;
                    player = other.gameObject;
                }
            }
        }
    }
}
