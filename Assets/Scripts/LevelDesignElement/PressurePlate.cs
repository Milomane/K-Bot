using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject lockedObject;
    public List<GameObject> onPlate = new List<GameObject>();
    public int limit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onPlate.Count < limit)
        {
            lockedObject.GetComponent<LockedDoor>().isActivated = false;
        }

        if (onPlate.Count >= limit)
        {
            lockedObject.GetComponent<LockedDoor>().isActivated = true; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onPlate.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        onPlate.Remove(other.gameObject);
    }
}
