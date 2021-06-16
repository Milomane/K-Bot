using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSphereRange : MonoBehaviour
{
    public List<ElectricalCapteurScript> allCaptorInRange;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ElectricalCapteur")
        {
            Debug.Log("Marche");
            other.GetComponent<ElectricalCapteurScript>().alimOn = true;
            allCaptorInRange.Add(other.GetComponent<ElectricalCapteurScript>());
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "ElectricalCapteur")
        {
            Debug.Log("Marche mais a l'envers");
            other.GetComponent<ElectricalCapteurScript>().alimOn = false;
            if (allCaptorInRange.Contains(other.GetComponent<ElectricalCapteurScript>()))
            {
                allCaptorInRange.Remove(other.GetComponent<ElectricalCapteurScript>());
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var captor in allCaptorInRange)
        {
            captor.alimOn = false;
        }
    }
}
