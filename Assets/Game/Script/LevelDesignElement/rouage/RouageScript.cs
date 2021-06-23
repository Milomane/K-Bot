using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RouageScript : MonoBehaviour
{
    public bool rotationOn, shutdown;
    public GameObject particul1, particul2;

    public float speedRotation;
    
    public UnityEvent eventShutDown;
    private AudioSource audioSource;

    private Animator anim;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        particul1.SetActive(false);
        particul2.SetActive(false);
        
        rotationOn = true;
    }
    
    void FixedUpdate()
    {
        if (rotationOn)
        {

        }
        
        if (!rotationOn && !shutdown)
        {
            eventShutDown.Invoke();
            anim.SetFloat("modeOff", 1);
            
            particul1.SetActive(true);
            particul2.SetActive(true);
            shutdown = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (rotationOn)
            {
                KillPlayer();
                audioSource.Stop();
            }
        }
        if (other.gameObject.CompareTag("Corpse"))
        {
            if (rotationOn)
            {
                rotationOn = false;
                Transform parentTransformCorp = other.gameObject.transform.parent;
                GameObject parentCorp = parentTransformCorp.gameObject;
                FindObjectOfType<PlayerDeathHandler>().DestroySelectedBody(parentCorp);
            }
        }
    }

    private void KillPlayer()
    {
        PlayerDeathHandler.instance.StartDeath(PlayerDeathHandler.DeathType.crunshed);
        rotationOn = false;
    }

    public void Activator()
    {
        rotationOn = true;
    }
    
    public void Desactivator()
    {
        rotationOn = false;
    }
}
