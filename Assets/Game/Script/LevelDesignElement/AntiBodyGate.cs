using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBodyGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // destroy all body when collide
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerDeathHandler>().DestroyAllBody();
        }
    }
}
