using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableMovingBody : MonoBehaviour
{
    public bool playerDetection;
    public GameObject player;
    public Transform playerGroup;
    public float timeBeforeActivation = 5f;


    public void Update()
    {
        timeBeforeActivation -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && timeBeforeActivation <= 0)
        {
            Debug.Log("oui");
            playerDetection = true;
            player = other.gameObject;
            playerGroup = player.transform.parent;
            playerGroup.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("non");
            playerDetection = false;
            playerGroup.transform.parent = null;
            player = null;
            playerGroup = null;
        }
    }
}
