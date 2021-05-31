using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltModule : MonoBehaviour
{
    public float speed;

    public Transform endPoint;

    public List<GameObject> onBelt;
    public GameObject player;
    public Transform playerGroup;

    public bool conveyorActivation;
    public bool x, z, mx, mz;
    
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
                onBelt[i].transform.position = Vector3.MoveTowards(onBelt[i].transform.position, endPoint.position, speed * Time.deltaTime);
            }

            if (player != null)
            {
                Debug.Log("le joueur bouge putain");
                if (x)
                {
                    player.transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                }
                else if (mx)
                {
                    player.transform.Translate(-Vector3.right * speed * Time.deltaTime, Space.World);
                    Debug.Log("aller bordel fils de pute bouge");
                }
                else if (mz)
                {
                    player.transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.World);
                }
                else if (z)
                {
                    player.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
                }
                
                //Vector3 movDiff = endPoint.position - transform.position;
                //Vector3 movDir = movDiff.normalized * speed * Time.deltaTime;
                //player.GetComponent<CharacterController>().Move(movDir);
                
                //player.transform.position = Vector3.MoveTowards(player.transform.position, endPoint.position, speed * Time.deltaTime);
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("oui");
            player = other.gameObject;
            //playerGroup = player.transform.parent;
            //playerGroup.transform.parent = transform;
            //onBelt.Add(other.gameObject);
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
            Debug.Log("non");
            //playerGroup.transform.parent = null;
            player = null;
            //playerGroup = null;
            
            //onBelt.Remove(other.gameObject);
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
