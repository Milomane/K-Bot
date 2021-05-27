using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public bool isActivated;

    public Vector3 open;

    public Vector3 closed;

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
           transform.rotation = Quaternion.Euler(open);
        }
        else
        {
            transform.rotation = Quaternion.Euler(closed);
        }
    }
}
