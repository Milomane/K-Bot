using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSphereRange : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ElectricalCapteur")
        {
            Debug.Log("Marche");
            other.GetComponent<ElectricalCapteurScript>().alimOn = true;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "ElectricalCapteur")
        {
            Debug.Log("Marche mais a l'envers");
            other.GetComponent<ElectricalCapteurScript>().alimOn = false;
        }
    }
}
