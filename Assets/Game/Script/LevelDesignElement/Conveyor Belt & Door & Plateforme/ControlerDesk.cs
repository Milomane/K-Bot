using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlerDesk : MonoBehaviour
{
    public GameObject imagePressCanvas;
    private bool ActivationOn;
    public bool timerDoorOn, activationTimer, verouillage;
    public float timerDoor, timer;
    public UnityEvent activationEvent, desactivationEvent;

    // Audio
    private AudioSource audioSource;
    [SerializeField] private AudioClip buttonOn;
    [SerializeField] private AudioClip buttonOff;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        imagePressCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (activationTimer && timerDoorOn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                desactivationEvent.Invoke();
                verouillage = false;
                activationTimer = false;
                
            }
        }
        if (ActivationOn)
        {
            if (Input.GetButtonDown("Interaction"))
            {
                audioSource.PlayOneShot(buttonOn);
                if (!verouillage)
                {
                    activationEvent.Invoke();
                    verouillage = true;
                    if (timerDoorOn) 
                    {
                        Debug.Log("timer");
                        timer = timerDoor;
                        activationTimer = true;
                    }
                    Debug.Log("activation");
                }
                else if (verouillage)
                {
                    audioSource.PlayOneShot(buttonOff);
                    desactivationEvent.Invoke();
                    verouillage = false;
                    Debug.Log("desactivation");
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
