using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;

    [Header("Movements")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float terminalVelocity = -25;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float airFriction = 2f;
    [SerializeField] private float secureJumpCd = .1f;
    public bool jumping;

    [Header("Ground")] 
    [SerializeField] private Transform groundDirection;
    [SerializeField] private Transform fallDirection;

    //Movement
    private float turnSmoothVelocity;
    private float verticalVelocity;
    private float jumpCd;
    private Vector3 inputNormalized;
    private float targetAngle;
    private float currentSpeed;
    private Vector3 moveDirection;
    
    //Jump
    private float jumpSpeed;
    private Vector3 jumpDirection;
    

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

    void Update()
    {
        isGrounded = controller.isGrounded;
        Locomotion();
        DebugGround();
    }

    void Locomotion()
    {
        GroundDirection();
        
        if (controller.isGrounded && slopeAngle <= controller.slopeLimit)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            inputNormalized = new Vector3(horizontal, 0f, vertical).normalized;

            if (Input.GetButton("Sprint"))
                currentSpeed = sprintSpeed;
            else
                currentSpeed = baseSpeed;
            
        }
        else if (!controller.isGrounded || slopeAngle > controller.slopeLimit)
        {
            inputNormalized = Vector2.Lerp(inputNormalized, Vector2.zero, (1/airFriction)*Time.deltaTime);
            currentSpeed = Mathf.Lerp(currentSpeed, 0, (1/airFriction)*Time.deltaTime);
        }
        
        if (jumping)
            moveDirection = jumpDirection;
        
        if (inputNormalized.magnitude >= .1f)
        {
            targetAngle = Mathf.Atan2(inputNormalized.x, inputNormalized.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = groundDirection.forward;
        }
        else
        {
            if (controller.isGrounded)
                moveDirection = Vector3.zero;
        }
        
        controller.Move(moveDirection.normalized * currentSpeed * forwardMult * Time.deltaTime);

        // Gravity
        if (!isGrounded && verticalVelocity > terminalVelocity)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else if (controller.isGrounded && slopeAngle > controller.slopeLimit)
        {
            verticalVelocity = Mathf.Lerp(verticalVelocity, terminalVelocity, .25f);
        }
            

        Vector3 fallVector;
        if (verticalVelocity <= 0)
        { 
            fallVector = fallDirection.up * verticalVelocity * fallMult;
        }
        else
        {
            fallVector = fallDirection.up * verticalVelocity;
        }
        
        controller.Move(fallVector * Time.deltaTime);
        
        if (controller.isGrounded && slopeAngle <= controller.slopeLimit)
        {
            if (jumping)
                jumping = false;
            verticalVelocity = -2f;
        }
        
        // Jump
        if (controller.isGrounded && Input.GetButtonDown("Jump") && jumpCd <= 0)
            Jump();
        
        jumpCd -= Time.deltaTime;
    }

    void Jump()
    {
        if (!jumping)
            jumping = true;

        jumpDirection = moveDirection;
        jumpSpeed = currentSpeed;
        
        verticalVelocity = jumpForce;
        jumpCd = secureJumpCd;
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
        collisionPoint = hit.point;
    }
}
