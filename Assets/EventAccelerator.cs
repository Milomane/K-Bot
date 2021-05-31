using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAccelerator : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touch");
            other.GetComponent<PlayerMovement>().SuperSprint();
        }
    }
}
