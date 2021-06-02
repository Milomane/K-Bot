using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonEventSender : MonoBehaviour
{
    public Piston piston;

    private void OnTriggerEnter(Collider other)
    {
        piston.TriggerEnterFromPiston(other);
    }
    private void OnTriggerExit(Collider other)
    {
        piston.TriggerExitFromPiston(other);
    }
}
