using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class EventDeath : MonoBehaviour
{
    [SerializeField] private bool doParticleAtDestruction;
    [SerializeField] private GameObject particleAtDestruction;
    public GameObject body;
    public UnityEvent eventsToActiveAtDestruction;

    public void DestroyBody()
    {
        Debug.Log("Hello?");
        
        if (doParticleAtDestruction)
        {
            if (particleAtDestruction != null)
                if (body != null)
                    Instantiate(particleAtDestruction, body.transform.position, quaternion.Euler(-90,0,0));
                else
                    Debug.LogError("Error in DestroyBody, body is null");
            else
                Debug.LogError("Error in DestroyBody, particleAtDestruction is null");
        }

        eventsToActiveAtDestruction.Invoke();
        
        Destroy(gameObject);
    }
}
