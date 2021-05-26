using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpringableBody : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool alignWithWorldUp;
    [SerializeField] private float alignSpeed = 80;
    private Quaternion targetRotation;
    
    void Start()
    {
        targetRotation = Quaternion.LookRotation(new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f)).normalized);
    }
    void Update()
    {
        if (alignWithWorldUp)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, alignSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Spring" && other.contacts[0].normal.y > .4f && other.contacts[0].normal.x < .5f && other.contacts[0].normal.z < .5f)
        {
            alignWithWorldUp = true;
            
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

            rb.AddForce(other.collider.GetComponent<Spring>().springForce * Vector3.up * 50);
            
            other.collider.GetComponent<Spring>().UseSpring();
        }
    }
    
    
}
