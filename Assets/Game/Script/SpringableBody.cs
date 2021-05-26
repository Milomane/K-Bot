using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringableBody : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Spring" && other.contacts[0].normal.y > .4f && other.contacts[0].normal.x < .5f && other.contacts[0].normal.z < .5f)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            //transform.rotation = Quaternion.RotateTowards();
            
            rb.AddForce(other.collider.GetComponent<Spring>().springForce * Vector3.up * 50);
            
            other.collider.GetComponent<Spring>().UseSpring();
        }
    }
}
