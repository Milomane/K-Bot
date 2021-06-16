using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public GameObject lockedObject;
    public List<GameObject> onPlate = new List<GameObject>();
    public int limit;
    public bool defineOpen;
    public UnityEvent openSystem, closeSystem;
    public bool verouillage;
    public LayerMask transparentForCamera;

    private bool playerOnPlate;
    private bool playerInAir;

    // Update is called once per frame 
    void Update()
    {
        if (PlayerDeathHandler.instance.dying )// player.GetComponent<PlayerMovement>().isGrounded == false ) // remove player when suicide
        {
            onPlate.Remove(PlayerDeathHandler.instance.gameObject);
        }

        for (int i = 0; i < onPlate.Count; i++) // remove destroy object from list
        {
            if (onPlate[i] == null)
            {
                onPlate.Remove(onPlate[i]);
            }  
        }

        playerInAir = !PlayerController.instance.playerMovement.isGrounded;
        int playerCount = 0;
        if (playerInAir && playerOnPlate)
            playerCount = -1;
        

        if (onPlate.Count + playerCount < limit + 1 && verouillage) // close door
        {
            //lockedObject.GetComponent<LockedDoor>().Close();
            closeSystem.Invoke();
            verouillage = false;
        }

        if (onPlate.Count + playerCount >= limit + 1 && !verouillage) // open door
        {
            //lockedObject.GetComponent<LockedDoor>().Open();
            openSystem.Invoke();
            verouillage = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Corpse"))
            onPlate.Add(other.gameObject);
        if (other.CompareTag("Player"))
            playerOnPlate = true;
    }
    

    private void OnTriggerExit(Collider other)
    {
        onPlate.Remove(other.gameObject);
        if (other.CompareTag("Player"))
            playerOnPlate = false;
    }
}
