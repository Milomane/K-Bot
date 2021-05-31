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

    private CharacterController playerCharacterController;
    private bool pushingPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
      //  StartCoroutine(Up());
    }

    // Update is called once per frame
    void Update()
    {
        if (activeUp)
        {
            StartCoroutine(Up());
            
        }

        if (activeDown )
        {
            StartCoroutine(Down());
            
        }
    }

    private IEnumerator Up()
    {
        activeUp = false;
        rb.AddForce(Vector3.left*200,ForceMode.Impulse);
        
        yield return new WaitForSeconds(0.25f);
        
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        activeDown = true;

    }
    
    private IEnumerator Down()
    {
        activeDown = false;
        
        rb.AddForce(Vector3.left*-50,ForceMode.Impulse);
        
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero; 
        yield return new  WaitForSeconds(2);
        activeUp = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCharacterController = other.GetComponent<CharacterController>();
            pushingPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pushingPlayer = false;
        }
    }

    private void FixedUpdate()
    {
        if (pushingPlayer)
            playerCharacterController.Move(rb.velocity * Time.fixedDeltaTime);
    }
}
