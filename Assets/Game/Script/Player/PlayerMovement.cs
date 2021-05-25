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
    [SerializeField] private float inAirSpeed = 3f;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float terminalVelocity = -25;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float secureJumpCd = .1f;
    public bool jumping;
    
    [Header("Accelerations")]
    [SerializeField] private float baseSpeedAcceleration = 12f;
    [SerializeField] private float sprintSpeedAcceleration = 20f;
    [SerializeField] private float inAirSpeedAcceleration = 6f;

    [SerializeField] private float groundDeceleration = .05f;
    [SerializeField] private float airDeceleration = 2f;

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
    private float currentAcceleration;
    private float currentDeceleration;
    private Vector3 moveDirection;
    private Vector3 moveSpeed;

    //Jump
    
    private Vector3 jumpDirection;
    private float springCd;
    

    //Ground and wall
    private Vector3 forwardDirection, collisionPoint, wallCollisionPoint;
    private float slopeAngle, forwardAngle, wallAngle;
    private float forwardMult;
    private float fallMult;
    private Ray groundRay, wallRay;
    private RaycastHit groundHit;
    private RaycastHit wallHit;
    public bool isHittingWall;
    
    //Debug
    [Header("Debug")] 
    public bool showGroundNormal;
    public bool showFallNormal;
    public bool isGrounded;
    public float realSpeed;
    

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
            if (inputNormalized != Vector3.zero)
            {
                // Check for sprint
                if (Input.GetButton("Sprint"))
                {
                    // Set speed to sprint speed
                    currentSpeed = sprintSpeed;
                    currentAcceleration = sprintSpeedAcceleration;
                }
                else
                {
                    // Set speed to base speed
                    currentSpeed = baseSpeed;
                    currentAcceleration = baseSpeedAcceleration;
                }
            }

            currentDeceleration = groundDeceleration;
            controller.stepOffset = .3f;
        }
        else if (!controller.isGrounded || slopeAngle > controller.slopeLimit)
        {
            WallDirection();
            
            // Decrease input and current speed with air friction if in air
            //inputNormalized = Vector2.Lerp(inputNormalized, Vector2.zero, (1/airFriction)*Time.deltaTime);
            currentSpeed = inAirSpeed;
            currentAcceleration = inAirSpeedAcceleration;
            currentDeceleration = airDeceleration;
            controller.stepOffset = 0f;
        }
        
        

        if (jumping)
        {
            // Apply jump direction to move direction while jumping
            moveDirection = jumpDirection;
        }
            
        
        if (inputNormalized.magnitude >= .1f)
        {
            // Smooth player angle and define moveDirection and DEFINE TARGET ANGLE
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

        if (inputNormalized.magnitude > .1f && Time.deltaTime != 0)
        {
            float changeApplied = currentAcceleration;
            float vectorDistance = Vector3.Distance(moveSpeed, moveDirection);
            moveSpeed = Vector3.Lerp(moveSpeed, moveDirection * currentSpeed, Mathf.Clamp(changeApplied * Time.deltaTime / vectorDistance, 0, 1));
        }
        else if (Time.deltaTime != 0)
        {
            moveSpeed = Vector3.Lerp(moveSpeed, Vector3.zero, Mathf.Clamp(currentDeceleration * Time.deltaTime / moveSpeed.magnitude, 0, 1));
        }

        if (isHittingWall)
        {
            /*float alpha = Vector3.Angle(Vector3.forward, wallDirectionInVector3);*/
            
            Vector3 wallDirectionVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * wallAngle), 0, Mathf.Cos(Mathf.Deg2Rad * wallAngle));

            moveSpeed = Vector3.Scale(moveSpeed, wallDirectionVector);
            isHittingWall = false;
        }
        moveSpeed = Vector3.ClampMagnitude(moveSpeed, sprintSpeed);

        realSpeed = moveSpeed.magnitude;
        // MOVE CHARACTER CONTROLLER 
        controller.Move(moveSpeed * forwardMult * Time.deltaTime);

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

    void WallDirection()
    {
        wallRay.origin = wallCollisionPoint - moveDirection.normalized * 0.05f;
        wallRay.direction = moveDirection;

        if (Physics.Raycast(wallRay, out wallHit, 0.15f))
        {
            wallAngle = Vector3.Angle(moveDirection.normalized, wallHit.normal) - 90;

            Debug.Log("Wall forward angle : " + wallAngle);


            float wallDistance = Vector3.Distance(wallRay.origin, wallHit.point);
            if (wallDistance <= .1f)
            {
                Vector3 wallCross = Vector3.Cross(wallHit.normal, moveDirection.normalized);
                Debug.Log("wallCross : " + wallCross);
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
        if (!isGrounded)
        {
            wallCollisionPoint = hit.point;
            isHittingWall = true;
        }
        else
        {
            isHittingWall = false;
        }
    }
}
