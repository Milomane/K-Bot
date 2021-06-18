using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapinPlaque : MonoBehaviour
{
    public bool grappin, move, verouillage;

    public GrapinModule module;
    public GameObject imagePressCanvas, player;
    private bool ActivationOn;
    
    // Audio source
    private AudioSource audioSource;

    [SerializeField] private AudioClip pressureOn;
    [SerializeField] private AudioClip pressureOff;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 0.7f;
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
                        verouillage = true;
                    }
                    else if(player.gameObject.GetComponent<PlayerController>().stopMovement)
                    {
                        player.gameObject.GetComponent<PlayerController>().stopMovement = false;
                        Debug.Log("press e pour false");
                        verouillage = false;
                    }
                    else
                    {
                        Debug.Log("flute c'est un bug la");
                    }
                }

                if (verouillage)
                {
                    float horizontal = Input.GetAxisRaw("Horizontal");
                    float vertical = Input.GetAxisRaw("Vertical");
                    module.InputMove(horizontal,vertical);
                    
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
                    audioSource.PlayOneShot(pressureOff, 1f);
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
                    audioSource.PlayOneShot(pressureOn, 1f);
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
