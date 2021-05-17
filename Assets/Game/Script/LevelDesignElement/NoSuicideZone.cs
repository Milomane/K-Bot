using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSuicideZone : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           GameObject player = other.gameObject;
           player.GetComponent<PlayerDeathHandler>().canDie = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            player.GetComponent<PlayerDeathHandler>().canDie = true;
        }
    }
}
