using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltModule : MonoBehaviour
{
    public float speed;
    
    public List<GameObject> onBelt;
    public GameObject player;
    public bool conveyorActivation, detectionPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (conveyorActivation)
        {
            for (int i = 0; i <= onBelt.Count -1; i++)
            {
                onBelt[i].transform.position = Vector3.MoveTowards(onBelt[i].transform.position, onBelt[i].transform.position + -transform.right, speed * Time.fixedDeltaTime);
            }

            if (detectionPlayer)
            {
                if (player != null)
                {
                    player.GetComponent<CharacterController>().Move(-transform.right * speed * Time.fixedDeltaTime);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            detectionPlayer = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
        else if (other.gameObject.CompareTag("Corpse"))
        {
            detectionPlayer = false;
            player = null;
            onBelt.Add(other.gameObject);
        }
        else
        {
            onBelt.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            detectionPlayer = false;
            Debug.Log("player quitte tapis");
            
        }
        else
        {
            onBelt.Remove(other.gameObject);
        }
    }

    public void DesactivationBoard()
    {
        conveyorActivation = false;
    }

    public void ActivationBoard()
    {
        conveyorActivation = true;
    }
}
