using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public GameObject acidParticle;

    [SerializeField] private AudioClip acidSound;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // destroy the player
        {
            AudioSource.PlayClipAtPoint(acidSound, other.gameObject.transform.position, 0.5f);
            other.gameObject.GetComponent<PlayerDeathHandler>().StartDeath(PlayerDeathHandler.DeathType.crunshed);
            Instantiate(acidParticle,other.transform.position + new Vector3(0,1,0), quaternion.Euler(-90,0,0));
        }

        if (other.CompareTag("Corpse")) // destroy the player body
        {
            AudioSource.PlayClipAtPoint(acidSound, other.gameObject.transform.position, 0.5f);
            Transform test = other.gameObject.transform.parent;
            PlayerDeathHandler.instance.DestroySelectedBody(test.gameObject);
            Instantiate(acidParticle,other.transform.position + new Vector3(0,1,0), quaternion.Euler(-90,0,0));
        }
    }
}
