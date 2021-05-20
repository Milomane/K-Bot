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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {
        if (player.GetComponent<PlayerDeathHandler>().dieing)
        {
            
            onPlate.Remove(player);
        }

        foreach (GameObject obj in onPlate)
        {
            if (obj == null)
            {
                onPlate.Remove(obj);
            }
        }
        
        if (onPlate.Count < limit + 1)
        {
            lockedObject.GetComponent<LockedDoor>().isActivated = !defineOpen;
        }

        if (onPlate.Count >= limit + 1)
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
