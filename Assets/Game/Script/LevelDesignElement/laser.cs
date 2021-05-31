using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer laser;
    public float distance;
    public bool defineOpen;
    public GameObject lockedObject;
    public bool itKilled;
    public GameObject player;
    public LayerMask layer;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit,1000, layer  ))
        {
            if (hit.collider.gameObject.CompareTag("Target")) // verify if touch a target and activate 
            {
                
                lockedObject.GetComponent<LockedDoor>().isActivated = defineOpen;
            }
            else
            {
                lockedObject.GetComponent<LockedDoor>().isActivated = !defineOpen;
            }

            if (itKilled && hit.collider.gameObject.CompareTag("Player") && player.GetComponent<PlayerDeathHandler>().dying == false) // kill the player
            {
                player.GetComponent<PlayerDeathHandler>().StartDeath(PlayerDeathHandler.DeathType.crunshed);
            }

            if (itKilled && hit.collider.gameObject.CompareTag("Corpse")) // destroy the player body
            {
                Transform test = hit.collider.gameObject.transform.parent;
                player.GetComponent<PlayerDeathHandler>().DestroySelectedBody(test.gameObject);
            }
            
            DrawRay(transform.position,hit.point);
        }
        else
        {
            DrawRay(transform.position, transform.forward * distance);
            lockedObject.GetComponent<LockedDoor>().isActivated = !defineOpen;  
        }
    }

    void DrawRay(Vector3 startPos, Vector3 endPos)
    {
        laser.SetPosition(0,startPos);
        laser.SetPosition(1,endPos);
    }
}
