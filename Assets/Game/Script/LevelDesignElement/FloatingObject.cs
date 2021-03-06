using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatingObject : MonoBehaviour
{
    public bool isInLiquid;
    public float liquidHeight = 0f;
    public float floatThreshold = .3f;
    public float liquidDensity = .5f;
    public float downForce = 4f;

    public float rotationSpeed = 20;

    public Rigidbody rb;

    private float forceFactor;
    private Vector3 floatForce;
    private Quaternion targetRotation;
    private float randomPointer;

    private void Start()
    {
        targetRotation = Quaternion.LookRotation(new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f)).normalized);
        randomPointer = Random.Range(0f, 100000f);
    }

    private void FixedUpdate()
    {
        if (isInLiquid)
        {
            forceFactor = 1f - ((transform.position.y - liquidHeight) / floatThreshold);

            if (forceFactor > 0)
            {
                floatForce = -Physics.gravity * (GetComponent<Rigidbody>().mass * (forceFactor - GetComponent<Rigidbody>().velocity.y * liquidDensity));
                floatForce += new Vector3(0f, -downForce * GetComponent<Rigidbody>().mass * (Mathf.PerlinNoise(randomPointer, 0f) * 1.5f) , 0f);
                GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
            }
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (GetComponent<Rigidbody>().velocity.x != 0 || GetComponent<Rigidbody>().velocity.z != 0)
                GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Lerp(GetComponent<Rigidbody>().velocity.x, 0, .1f), GetComponent<Rigidbody>().velocity.y, Mathf.Lerp(GetComponent<Rigidbody>().velocity.z, 0, .1f));
            
            

            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        
        
        randomPointer += Time.deltaTime;
    }
}
