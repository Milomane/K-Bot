using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    public LineRenderer laser;
    public float distance;
    public bool defineOpen;
    public GameObject lockedObject;
    public bool itKilled;
    public LayerMask layer;

    public UnityEvent eventActive, eventDesactive;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit,1000, layer  ))
        {
            if (hit.collider.gameObject.CompareTag("Target")) // verify if touch a target and activate 
            {
                
                eventActive.Invoke();
                Debug.Log("a");
            }
            else
            {
                eventDesactive.Invoke();
                Debug.Log("b");
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
            eventDesactive.Invoke();
            Debug.Log("c");
        }
    
    }

    void DrawRay(Vector3 startPos, Vector3 endPos)
    {
        laser.SetPosition(0,startPos);
        laser.SetPosition(1,endPos);
    }
}
