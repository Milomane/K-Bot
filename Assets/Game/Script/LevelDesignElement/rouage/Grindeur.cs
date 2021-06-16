using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grindeur : MonoBehaviour
{
    public bool activation, blocage;
    public List<GameObject> rotorGauche;
    public List<GameObject> rotorDroite;
    public float speed;
    
    public GameObject destroyParticle;

    private void FixedUpdate()
    {
        if (activation && !blocage)
        {
            for (int i = 0; i < rotorDroite.Count; i++)
            {
                rotorDroite[i].GetComponent<RotorGrindeur>().ActivationSystem();
            }
            for (int i = 0; i < rotorGauche.Count; i++)
            {
                rotorGauche[i].GetComponent<RotorGrindeur>().ActivationSystem();
            }

            blocage = true;
        }
        else if (!activation && blocage)
        {
            blocage = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activation)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerDeathHandler>().StartDeath(PlayerDeathHandler.DeathType.crunshed);
            }

            if (other.CompareTag("Corpse"))
            {
                Transform corpParent = other.gameObject.transform.parent;
                PlayerDeathHandler.instance.DestroySelectedBody(corpParent.gameObject);
            }  
        }
    }

    public void ActivationSystem()
    {
        activation = true;
    }
    public void DesactivationSystem()
    {
        activation = false;
    }
}
