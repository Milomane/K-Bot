using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryCannon : MonoBehaviour
{
    // Projectile compartment
    [SerializeField] private Transform projectileCompartment;
    
    // Fire power of the cannon
    [SerializeField] private float firePower = 2f;
    
    // Corpse
    private GameObject corpseKbot;
    
    // Is the cannon loaded
    private bool isCannonLoaded;

    public void Fire()
    {
        if (isCannonLoaded)
        {
            // Fire the corpseKbot
            Debug.Log("Fire !!!");
            corpseKbot.GetComponent<Rigidbody>().isKinematic = false;
            corpseKbot.GetComponent<Rigidbody>().velocity = firePower * corpseKbot.transform.forward;
            isCannonLoaded = false;
            // Play VFX (smoke, explosion, ...)
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Corpse") && !isCannonLoaded)
        {
            corpseKbot = other.gameObject;
            corpseKbot.transform.rotation = transform.rotation;
            
            // Cannon loading
            corpseKbot.transform.position = projectileCompartment.position;
            
            // Cannon is loaded
            isCannonLoaded = true;
            
            // Bloc the corpse
            corpseKbot.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
