using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject lockedObject;
    public List<GameObject> onPlate = new List<GameObject>();
    public int limit;
    public bool defineOpen;
    public GameObject player;

    // Update is called once per frame 
    void Update()
    {
        if (player.GetComponent<PlayerDeathHandler>().dieing )// player.GetComponent<PlayerMovement>().isGrounded == false ) // remove player when suicide
        {
            onPlate.Remove(player);
        }

        for (int i = 0; i < onPlate.Count; i++) // remove destroy object from list
        {
            if (onPlate[i] == null)
            {
                onPlate.Remove(onPlate[i]);
            }  
        } 
       
        
        if (onPlate.Count < limit + 1) // close door
        {
            lockedObject.GetComponent<LockedDoor>().isActivated = !defineOpen;
        }

        if (onPlate.Count >= limit + 1) // open door
        {
            lockedObject.GetComponent<LockedDoor>().isActivated = defineOpen; 
        }

        

       
    }

    private void OnTriggerEnter(Collider other)
    {
        onPlate.Add(other.gameObject);
        Debug.Log(other.gameObject.name);
    }
    

    private void OnTriggerExit(Collider other)
    {
        onPlate.Remove(other.gameObject);
    }
}
