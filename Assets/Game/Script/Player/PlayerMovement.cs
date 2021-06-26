using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;

    [Header("Movements")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float superSprintSpeed = 10f;
    [SerializeField] private float superSprintTime = 5f;
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
    [SerializeField] private float minAirTime = .1f;
    private float airTime;
    [SerializeField] private Transform groundDirection;
    [SerializeField] private Transform fallDirection;

    public bool stopMovement;
    public bool brutStopMovement;
    public bool sprint;

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
    
    // Super sprint
    public bool superSprint;
    private float superSprintTimer = 0;

    //Jump
    private Vector3 jumpDirection;
    private float springCd;
    private bool wasGrounded;
    

    //Ground and wall
    private Vector3 forwardDirection, collisionPoint, wallCollisionPoint;
    private float slopeAngle, forwardAngle, wallAngle;
    private float forwardMult;
    private float fallMult;
    private Ray groundRay, wallRay;
    private RaycastHit groundHit;
    private RaycastHit wallHit;
    public bool isHittingWall;
    
    // Audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip landSound;
    private bool doOneSound;
    
    //Debug
    [Header("Debug")] 
    public bool showGroundNormal;
    public bool showFallNormal;
    public bool isGrounded;
    public float realSpeed;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (wasGrounded && !isGrounded)
        {
            // Go off ground
        }
        else if (!wasGrounded && isGrounded && airTime > minAirTime)
        {
            // Back on Ground
            ReturnOnGround();
        }
        else
        {
            doOneSound = true;
        }

        if (!isGrounded)
            airTime += Time.deltaTime;
        else
            airTime = 0;
        
        playerController.animator.SetBool("InAir", !isGrounded && airTime > minAirTime);
        playerController.animator.SetBool("Jumping", jumping);
        
        DebugGround();
        
        // INPUTS
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        inputNormalized = new Vector3(horizontal, 0f, vertical).normalized;
        
        // Jump
        // Start jump if player is on ground and he press jump
        if (controller.isGrounded && Input.GetButtonDown("Jump") && jumpCd <= 0 && !stopMovement)
            Jump();

        bool eventSprint = sprint;
        
        // Super sprint bar
        if (superSprint)
        {
            superSprintTimer -= Time.deltaTime;
            CanvasEventManager.instance.accBarPivot.localScale = new Vector3(superSprintTimer / superSprintTime, 1, 1);
            eventSprint = true;
        }
        
        playerController.animator.SetBool("Sprint", eventSprint);
        
        // Decrease both timer
        jumpCd -= Time.deltaTime;
        springCd -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Locomotion();
    }

    void Locomotion()
    {
        GroundDirection();
        
        
        if (isGrounded && slopeAngle <= controller.slopeLimit)
        {
            if (inputNormalized != Vector3.zero)
            {
                // Check for sprint
                if (superSprint)
                {
                    // Set speed for super sprint
                    currentSpeed = superSprintSpeed;
                    currentAcceleration = sprintSpeedAcceleration;
                }
                else if (Input.GetButton("Sprint"))
                {
                    // Set speed to sprint speed
                    currentSpeed = sprintSpeed;
                    currentAcceleration = sprintSpeedAcceleration;
                    sprint = true;
                }
                else
                {
                    // Set speed to base speed
                    currentSpeed = baseSpeed;
                    currentAcceleration = baseSpeedAcceleration;
                    sprint = false;
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

        if ((inputNormalized.magnitude >= .1f || superSprint) && !stopMovement)
        {
            // Smooth player angle and define moveDirection and DEFINE TARGET ANGLE
            if (inputNormalized.magnitude >= .1f)
            {
                targetAngle = Mathf.Atan2(inputNormalized.x, inputNormalized.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            }
            
            float angleForRotation = Mathf.Atan2(inputNormalized.x, inputNormalized.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // In Case the user is not moving while super sprinting
            if (superSprint && inputNormalized.magnitude < .1f)
            {
                angleForRotation = Mathf.Atan2(groundDirection.forward.x, groundDirection.forward.z) * Mathf.Rad2Deg;
                
            }
                
            
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleForRotation, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = groundDirection.forward;
        }
        else
        {
            // If the player is not moving and grounded, the move direction reset
            /*if (controller.isGrounded)
                moveDirection = Vector3.zero;                    HERE*/
            
            moveDirection = Vector3.zero;
        }

        if ((inputNormalized.magnitude > .1f || superSprint) && !stopMovement && Time.deltaTime != 0)
        {
            playerController.animator.SetBool("Walk", true);
            float changeApplied = currentAcceleration;
            float vectorDistance = Vector3.Distance(moveSpeed, moveDirection);
            moveSpeed = Vector3.Lerp(moveSpeed, moveDirection * currentSpeed, Mathf.Clamp(changeApplied * Time.fixedDeltaTime / vectorDistance, 0, 1));
        }
        else if (Time.fixedDeltaTime != 0)
        {
            playerController.animator.SetBool("Walk", false);
            moveSpeed = Vector3.Lerp(moveSpeed, Vector3.zero, Mathf.Clamp(currentDeceleration * Time.fixedDeltaTime / moveSpeed.magnitude, 0, 1));
        }

        if (isHittingWall)
        {
            /*float alpha = Vector3.Angle(Vector3.forward, wallDirectionInVector3);*/
            
            Vector3 wallDirectionVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * wallAngle), 0, Mathf.Cos(Mathf.Deg2Rad * wallAngle));

            moveSpeed = Vector3.Scale(moveSpeed, wallDirectionVector);
            isHittingWall = false;
        }
        moveSpeed = Vector3.ClampMagnitude(moveSpeed, superSprintSpeed);

        realSpeed = moveSpeed.magnitude;
        
        // MOVE CHARACTER CONTROLLER 
        if (!brutStopMovement)
            controller.Move(moveSpeed * forwardMult * Time.fixedDeltaTime);

        // Calc vertical velocity with gravity
        if (!isGrounded && verticalVelocity > terminalVelocity)
        {
            verticalVelocity += gravity * Time.fixedDeltaTime;
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
            if (Physics.Raycast(transform.position + controller.center + Vector3.up * (1.85f / 2), Vector3.up, .1f))
            {
                verticalVelocity = 0;
            }
        }
        
        // Move Character controller down
        if (!brutStopMovement)
            controller.Move(fallVector * Time.fixedDeltaTime);
        
        // Enter if grounded and the slope is not to sharp
        // Also check for
        if (controller.isGrounded && slopeAngle <= controller.slopeLimit && springCd < 0)
        {
            // Stop jumping and stop velocity
            if (jumping)
                jumping = false;
            verticalVelocity = -2f;
        }
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

    public void SuperSprint()
    {
        if (!superSprint && !playerController.dying)
            StartCoroutine(SuperSprintCoroutine());
    }

    IEnumerator SuperSprintCoroutine()
    {
        superSprint = true;
        CanvasEventManager.instance.accBarObject.SetActive(true);
        superSprintTimer = superSprintTime;
        yield return new WaitForSeconds(superSprintTime);
        superSprint = false;
        CanvasEventManager.instance.accBarObject.SetActive(false);
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
        
        if (inputNormalized.magnitude > 0 || superSprint)
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


            float wallDistance = Vector3.Distance(wallRay.origin, wallHit.point);
            if (wallDistance <= .1f)
            {
                Vector3 wallCross = Vector3.Cross(wallHit.normal, moveDirection.normalized);
            }
        }
    }

    public void ReturnOnGround()
    {
        // GetComponentInChildren<PlayerSounds>().Land(); TODO : résoudre le problème de son
        if (doOneSound)
        {
            audioSource.PlayOneShot(landSound);
            doOneSound = false;
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
        
        // Handle Springs
        if (hit.collider.tag == "Spring" && hit.normal.y > .4f && hit.normal.x < .5f && hit.normal.z < .5f && verticalVelocity < 0)
        {
            SpringJump(hit.collider.GetComponent<Spring>().springForce);
            
            hit.collider.GetComponent<Spring>().UseSpring();
        } else if (!isGrounded)
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
