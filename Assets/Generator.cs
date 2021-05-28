using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public float radius = 10f;
    public Transform sphereRangeTransform;

    public void Start()
    {
        sphereRangeTransform.localScale = new Vector3(radius, radius, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ElectricalCapteur")
        {
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ElectricalCapteur")
        {
            
        }
    }
}
