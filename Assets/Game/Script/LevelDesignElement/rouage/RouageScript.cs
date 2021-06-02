using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouageScript : MonoBehaviour
{
    public bool rotationOn, corpDetection;

    public float speedRotation;
    public GameObject player;
    private MeshCollider _meshCollider;

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        rotationOn = true;
    }
    
    void FixedUpdate()
    {
        if (!corpDetection)
        {
            if (rotationOn)
            {
                transform.Rotate(0,speedRotation * Time.deltaTime ,0);
                _meshCollider.convex = true;
                _meshCollider.isTrigger = true;
            } 
        }

        if (corpDetection)
        {
            _meshCollider.convex = false;
            _meshCollider.isTrigger = false;
        }
        else if (!rotationOn)
        {
            _meshCollider.convex = false;
            _meshCollider.isTrigger = false;
        }
        else if (corpDetection && !rotationOn)
        {
            _meshCollider.convex = false;
            _meshCollider.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (rotationOn && !corpDetection)
            {
                Debug.Log("contact rouage player");
                player = other.gameObject;
                KillPlayer();
            }
        }
        if (other.gameObject.CompareTag("Corpse"))
        {
            corpDetection = true;
        }
    }

    private void KillPlayer()
    {
        Debug.Log("kill rouage player");
        player.GetComponent<PlayerDeathHandler>().StartDeath(PlayerDeathHandler.DeathType.crunshed);
        player = null;
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
