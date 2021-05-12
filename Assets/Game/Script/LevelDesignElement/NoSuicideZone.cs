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
            Debug.Log("ne peut plus mourir"); // fonction
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(" peut mourir"); // fonction
        }
    }
}
