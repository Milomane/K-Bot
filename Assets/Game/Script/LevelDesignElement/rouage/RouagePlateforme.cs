using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouagePlateforme : MonoBehaviour
{
    public bool rouageActivation, playerDetection;
    public float speedRotation;
    public bool rotationHoraire;

    public GameObject player;

    public Transform playerGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rouageActivation)
        {
            if (rotationHoraire)
            {
                transform.Rotate(0,speedRotation * Time.fixedDeltaTime ,0);
            }
            else
            {
                transform.Rotate(0,-speedRotation * Time.fixedDeltaTime ,0);
            }
            
        }
    }
    public void DesactivationBoard()
    {
        rouageActivation = false;
    }

    public void ActivationBoard()
    {
        rouageActivation = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
