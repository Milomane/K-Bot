using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCollectable : MonoBehaviour
{
    public bool collectable = true;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && collectable)
        {
            PlayerController.instance.IncrementStatue();
            Destroy(gameObject);
        }
    }
}
