using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public GameObject[] lamp;

    public bool activate;
    public float distance;
    public GameObject lockedObj;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        lamp = GameObject.FindGameObjectsWithTag("Lamp");

        if (lamp != null)
        {
            foreach (GameObject obj in lamp)
            {
                float test = Vector3.Distance(transform.position, obj.transform.position);
                if (test <= distance)
                {
                    activate = true;
                    lockedObj.GetComponent<LockedDoor>().isActivated = activate;
                }
              
            }
        }
    }
}
