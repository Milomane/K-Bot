using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RouageScript : MonoBehaviour
{
    public bool rotationOn, shutdown;

    public float speedRotation;
    private MeshCollider _meshCollider;
    public UnityEvent eventShutDown;

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        rotationOn = true;
    }
    
    void FixedUpdate()
    {
        
        if (rotationOn)
        {
            transform.Rotate(0,speedRotation * Time.deltaTime ,0);
            _meshCollider.convex = true;
            _meshCollider.isTrigger = true;
        } 
        
        
        if (!rotationOn && !shutdown)
        {
            eventShutDown.Invoke();
            _meshCollider.isTrigger = false;
            _meshCollider.convex = false;
            shutdown = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (rotationOn)
            {
                KillPlayer();
            }
        }
        if (other.gameObject.CompareTag("Corpse"))
        {
            if (rotationOn)
            {
                rotationOn = false;
                Transform parentTransformCorp = other.gameObject.transform.parent;
                GameObject parentCorp = parentTransformCorp.gameObject;
                FindObjectOfType<PlayerDeathHandler>().DestroySelectedBody(parentCorp);
            }
        }
    }

    private void KillPlayer()
    {
        PlayerDeathHandler.instance.StartDeath(PlayerDeathHandler.DeathType.crunshed);
        rotationOn = false;
    }

    public void Activator()
    {
        rotationOn = true;
    }
    
    public void Desactivator()
    {
        rotationOn = false;
    }
}
