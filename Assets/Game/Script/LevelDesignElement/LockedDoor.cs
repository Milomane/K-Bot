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
      
    }


    public void Open()
    {
        transform.rotation = Quaternion.Euler(open);
    }

    public void Close()
    {
        transform.rotation = Quaternion.Euler(closed); 
    }
}
