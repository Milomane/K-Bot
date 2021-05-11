using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float secureJumpCd = .1f;
    

    private float turnSmoothVelocity;
    private float verticalVelocity;
    private float jumpCd;

    private bool wasGrounded;

    private Vector3 hitNormal;
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 velocity = Vector3.zero;
        

        if (direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            

            velocity = moveDirection.normalized * speed;
        }

        Vector3 verticalVector = new Vector3(0, verticalVelocity, 0);

        velocity += verticalVector;
        velocity *= Time.deltaTime;

        controller.Move(velocity);
        
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && jumpCd <= 0)
            {
                verticalVelocity = jumpForce;
                jumpCd = secureJumpCd;
            }

            jumpCd -= Time.deltaTime;
            
            if (wasGrounded) verticalVelocity += gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }
    
    void OnControllerColliderHit (ControllerColliderHit hit) {
        hitNormal = hit.normal;
        
    }
}
