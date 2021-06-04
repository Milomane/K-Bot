using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // destroy the player
        {
            other.gameObject.GetComponent<PlayerDeathHandler>().StartDeath(PlayerDeathHandler.DeathType.crunshed);
        }

        if (other.CompareTag("Corpse")) // destroy the player body
        {
            Transform test = other.gameObject.transform.parent;
            PlayerDeathHandler.instance.DestroySelectedBody(test.gameObject);
        }
    }
}
