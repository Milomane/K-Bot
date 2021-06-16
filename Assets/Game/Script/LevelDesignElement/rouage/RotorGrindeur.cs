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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activation)
        {
            if (left)
            {
                transform.Rotate(0,0,speed * Time.fixedDeltaTime);
            }
            else
            {
                transform.Rotate(0,0,-speed * Time.fixedDeltaTime);
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
