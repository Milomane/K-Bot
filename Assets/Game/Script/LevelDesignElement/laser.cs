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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        Debug.DrawRay(transform.position,transform.forward,Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward, out hit ))
        {
            if (hit.collider.gameObject.CompareTag("Target"))
            {
                Debug.Log("test");
                lockedObject.GetComponent<LockedDoor>().isActivated = defineOpen;
            }

            if (itKilled && hit.collider.gameObject.CompareTag("Player") && player.GetComponent<PlayerDeathHandler>().dieing == false)
            {
                player.GetComponent<PlayerDeathHandler>().StartDeath(PlayerDeathHandler.selectedDeath);
            }

            if (itKilled && hit.collider.gameObject.CompareTag("Corpse"))
            {
                Transform test = hit.collider.gameObject.transform.parent;
                player.GetComponent<PlayerDeathHandler>().DestroySelectedBody(test.gameObject);
            }
            
            DrawRay(transform.position,hit.point);
        }
        else
        {
            DrawRay(transform.position, transform.forward * distance);
        }
    }

    void DrawRay(Vector3 startPos, Vector3 endPos)
    {
        laser.SetPosition(0,startPos);
        laser.SetPosition(1,endPos);
    }
}
