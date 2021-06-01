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
                onBelt[i].transform.position = Vector3.MoveTowards(onBelt[i].transform.position, endPoint.position, speed * Time.fixedDeltaTime);
            }

            if (player != null)
            {
                if (x)
                {
                    Vector3 direction = new Vector3(1,0,0);
                    player.GetComponent<CharacterController>().Move(direction * speed * Time.fixedDeltaTime);
                }
                else if (mx)
                {
                    Vector3 direction = new Vector3(-1,0,0);
                    player.GetComponent<CharacterController>().Move(direction * speed * Time.fixedDeltaTime);
                }
                else if (mz)
                {
                    Vector3 direction = new Vector3(0,0,-1);
                    player.GetComponent<CharacterController>().Move(direction * speed * Time.fixedDeltaTime);
                }
                else if (z)
                {
                    Vector3 direction = new Vector3(0,0,1);
                    player.GetComponent<CharacterController>().Move(direction * speed * Time.fixedDeltaTime);
                }
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
