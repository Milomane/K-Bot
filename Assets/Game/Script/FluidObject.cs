using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FloatingObject>())
        {
            other.GetComponent<FloatingObject>().isInLiquid = true;
            other.GetComponent<FloatingObject>().liquidHeight = transform.position.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FloatingObject>())
        {
            other.GetComponent<FloatingObject>().isInLiquid = false;
        }
    }
}
