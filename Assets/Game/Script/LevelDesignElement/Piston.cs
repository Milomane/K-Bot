using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField] private bool activeUp;
    [SerializeField] private bool activeDown;
    private Vector3 velocity = Vector3.one;

    public Rigidbody rb;


    private Transform playerCameraGroup;
    private CharacterController playerCharacterController;
    private bool pushingPlayer;
    private bool pushing;

    private Vector3 lastPos;
    public Transform animatedTransform;
    public Transform pistonObject;

    
    void Start()
    {
    }

    
    void Update()
    {
    }

    public void PistonEndMovement()
    {   
        pushing = false;
    }

    public void PistonStartMovement()
    {
        pushing = true;
    }

    public void TriggerEnterFromPiston(Collider other)
    {
        if (other.tag == "Player")
        {
            
            playerCharacterController = other.GetComponent<CharacterController>();
            
            pushingPlayer = true;
            
        }
    }
    public void TriggerExitFromPiston(Collider other)
    {
        if (other.tag == "Player")
        {
            pushingPlayer = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = rb.position - lastPos;
        
        rb.MovePosition(animatedTransform.position);

        if (pushingPlayer && pushing)
        {
            playerCharacterController.Move(rb.velocity * Time.fixedDeltaTime);
            //Debug.Log(rb.velocity);
        }
        else
        {
            //playerCameraGroup.parent = null;
        }
        
        lastPos = rb.position;
    }
}
