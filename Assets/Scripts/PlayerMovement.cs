using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;

    [Header("Movements")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float secureJumpCd = .1f;

    [Header("Ground")] 
    [SerializeField] private Transform groundDirection;
    
    //Movement
    private float turnSmoothVelocity;
    private float verticalVelocity;
    private float jumpCd;
    private Vector3 inputNormalized;

    //Ground
    private Vector3 forwardDirection;
    private float slopeAngle;
    private Ray groundRay;
    private RaycastHit groundHit;
    
    void Update()
    {
        Locomotion();
        
    }

    void Locomotion()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        inputNormalized = new Vector3(horizontal, 0f, vertical).normalized;
        
        GroundDirection();
        
        if (inputNormalized.magnitude >= .1f)
        {
            //Test for in air movement -> to do
            if (controller.isGrounded) ;
            
            
            float targetAngle = Mathf.Atan2(inputNormalized.x, inputNormalized.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        
        GravityAndJump();
    }

    void GravityAndJump()
    {
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 verticalVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(verticalVector * Time.deltaTime);
        
        
        if (controller.isGrounded)
        {
            verticalVelocity = -2f;
            if (Input.GetButtonDown("Jump") && jumpCd <= 0)
            {
                verticalVelocity = jumpForce;
                jumpCd = secureJumpCd;
            }
        }
        jumpCd -= Time.deltaTime;
    }

    void GroundDirection()
    {
        forwardDirection = transform.position;
        if (inputNormalized.magnitude > 0)
            forwardDirection += transform.forward * inputNormalized.x + transform.right * inputNormalized.z;
        else
            forwardDirection += transform.forward;

        groundDirection.LookAt(forwardDirection);

        groundRay.origin = transform.position += Vector3.up * 0.05f;
        groundRay.direction = Vector3.down;

        if (Physics.Raycast(groundRay, out groundHit, 0.75f))
        {
            slopeAngle = Vector3.Angle(Vector3.up, groundHit.normal);
        }
    }
}
