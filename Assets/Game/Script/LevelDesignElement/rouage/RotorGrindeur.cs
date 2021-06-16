using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotorGrindeur : MonoBehaviour
{
    public bool left, activation;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 70f;
    }

    // Update is called once per frame
    void Update()
    {
        if (activation)
        {
            if (left)
            {
                transform.RotateAround(transform.position,Vector3.up, -speed * Time.deltaTime);
            }
            else
            {
                
            }
        }
    }
    public void ActivationSystem()
    {
        activation = true;
    }
    public void DesactivationSystem()
    {
        activation = false;
    }
}
