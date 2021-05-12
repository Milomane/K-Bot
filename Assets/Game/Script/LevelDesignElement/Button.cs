using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject lockedObject;

    public float distRequired;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
        Debug.Log(distance);

        if (distance >= distRequired && Input.GetButton("Jump"))
        {
            lockedObject.GetComponent<LockedDoor>().isActivated = true;
        }
    }
}
