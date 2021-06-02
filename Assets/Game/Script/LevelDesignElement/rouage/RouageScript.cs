using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouageScript : MonoBehaviour
{
    public bool rotationOn, corpDetection;

    public float speedRotation;
    public GameObject player;

    private void Start()
    {
        rotationOn = true;
    }
    
    void FixedUpdate()
    {
        if (!corpDetection)
        {
            if (rotationOn)
            {
                transform.Rotate(0,speedRotation * Time.deltaTime ,0);
            } 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (rotationOn)
            {
                Debug.Log("contact rouage player");
                player = other.gameObject;
                KillPlayer();
            }
        }
        if (other.gameObject.CompareTag("Corpse"))
        {
            corpDetection = true;
        }
    }

    private void KillPlayer()
    {
        Debug.Log("kill rouage player");
        player.GetComponent<PlayerDeathHandler>().StartDeath(PlayerDeathHandler.DeathType.crunshed);
        player = null;
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
