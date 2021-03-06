using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltModule : MonoBehaviour
{
    public float speed;
    public List<ConveyorOffset> listOffset;
    public List<GameObject> onBelt;
    public GameObject player;
    public bool conveyorActivation, detectionPlayer, blocageOffset;

    // Start is called before the first frame update
    void Start()
    {
        float valueoffset;
        valueoffset = speed / 2;
        for (int i = 0; i < listOffset.Count; i++)
        {
            listOffset[i].scrollSpeed = valueoffset;
        }
        
        InvokeRepeating("checkMissingObject",2f,1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            if (detectionPlayer)
            {
                if (Input.GetButtonDown("Kill"))
                {
                    detectionPlayer = false;
                    player = null;
                }
            }
        }
        
        if (conveyorActivation)
        {
            for (int i = 0; i <= onBelt.Count -1; i++)
            {
                onBelt[i].transform.position = Vector3.MoveTowards(onBelt[i].transform.position, onBelt[i].transform.position + -transform.right, speed * Time.fixedDeltaTime);
            }

            if (!blocageOffset)
            {
                for (int i = 0; i < listOffset.Count ; i++)
                {
                    listOffset[i].activationOffset = true;
                }

                blocageOffset = true;
            }

            if (detectionPlayer)
            {
                if (player != null)
                {
                    player.GetComponent<CharacterController>().Move(-transform.right * speed * Time.fixedDeltaTime);
                }
            }
        }

        if (!conveyorActivation)
        {
            if (blocageOffset)
            {
                for (int i = 0; i < listOffset.Count ; i++)
                {
                    listOffset[i].activationOffset = false;
                }

                blocageOffset = false;
            }
        }
        if (onBelt.Count > 0)
        {
        }

        if(onBelt.Count >= 1)
        {
            onBelt.RemoveAll(GameObject => gameObject == null);
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
        else if (other.gameObject.CompareTag("Corpse") || other.gameObject.CompareTag("Lamp"))
        {
            detectionPlayer = false;
            player = null;
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

    void checkMissingObject()
    {
        for (int i = 0; i <= onBelt.Count -1; i++)
        {
            if (onBelt[i] == null)
            {
                onBelt.RemoveAt(i);
            }
        }
    }
}
