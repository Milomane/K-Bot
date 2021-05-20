using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementBackup : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;

    [Header("Movements")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float inAirSpeed = 3f;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float terminalVelocity = -25;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float airFriction = 2f;
    [SerializeField] private float secureJumpCd = .1f;
    public bool jumping;
    
    [Header("Accelerations")]
    [SerializeField] private float baseSpeedAcceleration = 12f;
    [SerializeField] private float sprintSpeedAcceleration = 20f;
    [SerializeField] private float inAirSpeedAcceleration = 6f;

    [SerializeField] private float groundFriction = .05f;

    [Header("Ground")] 
    [SerializeField] private Transform groundDirection;
    [SerializeField] private Transform fallDirection;

    public bool stopMovement;

    //Movement
    private float turnSmoothVelocity;
    private float verticalVelocity;
    private float jumpCd;
    private Vector3 inputNormalized;
    private float targetAngle;
    private float currentSpeed;
    private Vector3 moveDirection;
    private Vector3 moveSpeed;
    
    //Jump
    
    private Vector3 jumpDirection;
    private float springCd;
    

    //Ground
    private Vector3 forwardDirection, collisionPoint;
    private float slopeAngle, forwardAngle;
    private float forwardMult;
    private float fallMult;
    private Ray groundRay;
    private RaycastHit groundHit;
    
    //Debug
    [Header("Debug")] 
    public bool showGroundNormal;
    public bool showFallNormal;
    public bool isGrounded;
    public Transform testVectorTransform;
    public Vector3 testVector;
    public Vector3 testVectorTargetValue;
    public float testVectorLerpSpeed;
    public float testValue;
    public bool testActive;

    void Update()
    {
        isGrounded = controller.isGrounded;
        Locomotion();
        DebugGround();
    }

    void Locomotion()
    {
        GroundDirection();
        
        // INPUTS
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        inputNormalized = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (controller.isGrounded && slopeAngle <= controller.slopeLimit && !stopMovement)
        {
            // Set speed to sprint

            if (inputNormalized != Vector3.zero)
            {
                if (Input.GetButton("Sprint"))
                {
                    currentSpeed += sprintSpeedAcceleration * Time.deltaTime;
                    currentSpeed = Mathf.Clamp(currentSpeed, 0, sprintSpeed);
                }
                else
                {
                    currentSpeed += baseSpeedAcceleration * Time.deltaTime;
                    currentSpeed = Mathf.Clamp(currentSpeed, 0, baseSpeed);
                }
            }
            else
                currentSpeed = Mathf.Lerp(currentSpeed, 0, (1/groundFriction) * Time.deltaTime);
        }
        else if (!controller.isGrounded || slopeAngle > controller.slopeLimit)
        {
            // Decrease input and current speed with air friction if in air
            //inputNormalized = Vector2.Lerp(inputNormalized, Vector2.zero, (1/airFriction)*Time.deltaTime);
            currentSpeed = Mathf.Lerp(currentSpeed, 0, (1/airFriction) * Time.deltaTime);
            if (currentSpeed < 0)
                currentSpeed = 0;
        }
        
        

        if (jumping)
        {
            // Apply jump direction to move direction while jumping
            moveDirection = jumpDirection;
        }
            
        
        if (inputNormalized.magnitude >= .1f)
        {
            // Smooth Rotate player and define moveDirection
            targetAngle = Mathf.Atan2(inputNormalized.x, inputNormalized.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = groundDirection.forward;
        }
        else
        {
            // If the player is not moving and grounded, the move direction reset
            /*if (controller.isGrounded)
                moveDirection = Vector3.zero;                    HERE*/
        }
        
        // MOVE CHARACTER CONTROLLER 
        controller.Move(moveDirection.normalized * currentSpeed * forwardMult * Time.deltaTime);

        // Calc vertical velocity with gravity
        if (!isGrounded && verticalVelocity > terminalVelocity)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else if (controller.isGrounded && slopeAngle > controller.slopeLimit)
        {
            verticalVelocity = Mathf.Lerp(verticalVelocity, terminalVelocity, .25f);
        }

        // Calc fall direction because au slopes
        Vector3 fallVector;
        if (verticalVelocity <= 0)
        { 
            fallVector = fallDirection.up * verticalVelocity * fallMult;
        }
        else
        {
            fallVector = fallDirection.up * verticalVelocity;
        }
        
        // Move Character controller down
        controller.Move(fallVector * Time.deltaTime);
        
        // Enter if grounded and the slope is not to sharp
        // Also check for
        if (controller.isGrounded && slopeAngle <= controller.slopeLimit && springCd < 0)
        {
            // Stop jumping and stop velocity
            if (jumping)
                jumping = false;
            verticalVelocity = -2f;
        }
        
        // Jump
        // Start jump if player is on ground and he press jump
        if (controller.isGrounded && Input.GetButtonDown("Jump") && jumpCd <= 0)
            Jump();
        
        // Decrease both timer
        jumpCd -= Time.deltaTime;
        springCd -= Time.deltaTime;
    }

    void Jump()
    {
        if (!jumping)
            jumping = true;

        // Apply actual direction for the jump and set vertical velocity
        jumpDirection = moveDirection;
        verticalVelocity = jumpForce;
        
        // Start jump timer
        jumpCd = secureJumpCd;
    }

    public void SpringJump(float springForce)
    {
        jumping = true;

        jumpDirection = moveDirection;
        
        verticalVelocity = springForce;
        springCd = .1f;
    }

    void GroundDirection()
    {
        //Setting forward direction
        forwardDirection = transform.position;
        
        if (inputNormalized.magnitude > 0)
            forwardDirection += new Vector3(Mathf.Sin(Mathf.Deg2Rad * targetAngle), 0, Mathf.Cos(Mathf.Deg2Rad * targetAngle));
        else
            forwardDirection += transform.forward;

        // Seting ground direction
        groundDirection.LookAt(forwardDirection);
        fallDirection.rotation = transform.rotation;

        // Setting ground ray
        groundRay.origin = collisionPoint + Vector3.up * 0.05f;
        groundRay.direction = Vector3.down;

        forwardMult = 1;
        fallMult = 1;

        if (Physics.Raycast(groundRay, out groundHit, 0.55f))
        {
            // Get slop angle values
            slopeAngle = Vector3.Angle(Vector3.up, groundHit.normal);
            forwardAngle = Vector3.Angle(groundDirection.forward, groundHit.normal) - 90;
            
            if (forwardAngle < 0 && slopeAngle <= controller.slopeLimit)
            {
                // Setting Speed multiplier for matching speed one slope
                forwardMult = 1 / Mathf.Cos(forwardAngle * Mathf.Deg2Rad);
                
                // Setting ground direction to match down slope
                groundDirection.eulerAngles += new Vector3(-forwardAngle, 0, 0);
            } 
            else if (slopeAngle > controller.slopeLimit)
            {
                float groundDistance = Vector3.Distance(groundRay.origin, groundHit.point);
                if (groundDistance <= .1f)
                {
                    fallMult = 1 / Mathf.Cos((90 - slopeAngle) * Mathf.Deg2Rad);
                
                    Vector3 groundCross = Vector3.Cross(groundHit.normal, Vector3.up);
                    fallDirection.rotation = Quaternion.FromToRotation(transform.up, Vector3.Cross(groundCross, groundHit.normal));
                }
            }
        }
    }

    void DebugGround()
    {
        groundDirection.GetChild(0).gameObject.SetActive(showGroundNormal);
        fallDirection.GetChild(0).gameObject.SetActive(showFallNormal);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Handle Springs
        if (hit.collider.tag == "Spring")
        {
            Debug.Log(hit.normal);
        }
        if (hit.collider.tag == "Spring" && hit.normal.y > .4f && hit.normal.x < .5f && hit.normal.z < .5f && jumping)
        {
            SpringJump(hit.collider.GetComponent<Spring>().springForce);
            
            hit.collider.GetComponent<Spring>().UseSpring();
        }
            
        
        collisionPoint = hit.point;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(testVectorTransform.position, .05f);
        Vector3 linePos = testVector;
        if (testActive)
        {
            testValue += testVectorLerpSpeed * Time.deltaTime;
            if (testValue > 1) testValue = 1;
            linePos = Vector3.Lerp(testVector, testVectorTargetValue, testValue);
        }
        
        Gizmos.DrawLine(testVectorTransform.position, testVectorTransform.position + linePos);
    }
}