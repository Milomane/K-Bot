using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSphereRange : MonoBehaviour
{
    public List<ElectricalCapteurScript> allCaptorInRange;

    public void Start()
    {
        allCaptorInRange = new List<ElectricalCapteurScript>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ElectricalCapteur")
        {
            other.GetComponent<ElectricalCapteurScript>().alimOn++;
            allCaptorInRange.Add(other.GetComponent<ElectricalCapteurScript>());
            Debug.Log("Marche");
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "ElectricalCapteur")
        {
            other.GetComponent<ElectricalCapteurScript>().alimOn--;
            if (allCaptorInRange.Contains(other.GetComponent<ElectricalCapteurScript>()))
            {
                allCaptorInRange.Remove(other.GetComponent<ElectricalCapteurScript>());
            }
        }
    }

    public void AtDestruction()
    {
        if (allCaptorInRange.Count > 0)
            foreach (var captor in allCaptorInRange)
            {
                captor.alimOn--;
            }
    }

    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }
}
