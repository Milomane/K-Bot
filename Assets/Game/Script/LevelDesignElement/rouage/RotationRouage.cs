using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRouage : MonoBehaviour
{
    public bool rotationOn;
    public float speedRotation;

    private Vector3 currentEulerAngles;
    // Start is called before the first frame update
    void Start()
    {
        rotationOn = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotationOn)
        {
            currentEulerAngles += new Vector3(1,0,0) * Time.deltaTime * speedRotation;
            transform.localEulerAngles = currentEulerAngles;
        } 
    }

    public void rotationRotorOff()
    {
        rotationOn = false;
    }
}
