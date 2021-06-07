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
    public LayerMask layer;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit,1000, layer  ))
        {
            if (hit.collider.gameObject.CompareTag("Target")) // verify if touch a target and activate 
            {
                
                lockedObject.GetComponent<LockedDoor>().Open();
            }
            else
            {
                lockedObject.GetComponent<LockedDoor>().Close();
            }

            if (itKilled && hit.collider.gameObject.CompareTag("Player") && PlayerDeathHandler.instance.dying == false) // kill the player
            {
                PlayerDeathHandler.instance.StartDeath(PlayerDeathHandler.DeathType.crunshed);
            }

            if (itKilled && hit.collider.gameObject.CompareTag("Corpse")) // destroy the player body
            {
                Transform test = hit.collider.gameObject.transform.parent;
                PlayerDeathHandler.instance.DestroySelectedBody(test.gameObject);
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
