using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float cubeDiameter = 1f;
    public float maxHeightOnEdge = .45f;
    
    public float springForce = 10;
    public Animator testAnimator;

    public LayerMask layer;

    private Vector3 upRight;
    private Vector3 upLeft;
    private Vector3 downRight;
    private Vector3 downLeft;
    
    private Vector3 upRightHitCoord;
    private Vector3 upLeftHitCoord;
    private Vector3 downRightHitCoord;
    private Vector3 downLeftHitCoord;

    private float endYCoord;
    private bool canStartMoving;

    private float speed;
    public float terminalSpeed = 20f;

    public GameObject bouncingCube;
    public Transform bouncingCubeTransform;
    private GameObject oldFrame;



    public void UseSpring()
    {
        FindObjectOfType<PlayerSounds>().Spring();
        testAnimator.SetTrigger("Use");
    }

    public void Start()
    {
        CheckHeight();
    }
    
    /*
    public void ActiveBouncingCube(GameObject oldFrame)
    {
        bouncingCube.SetActive(true);
        this.oldFrame = oldFrame;
    }
    */

    public void RestoreFrame()
    {
        if (oldFrame != null)
        {
            oldFrame.SetActive(true);
            oldFrame.transform.position = bouncingCubeTransform.position;
        }
    }

    public void CheckHeight()
    { 
        float lowestYPoint;
        float highestYPoint;
        
        upRight = transform.position + transform.right * cubeDiameter/2f + transform.forward * cubeDiameter/2 + -transform.up * cubeDiameter/2f + transform.up;
        upLeft = transform.position + -transform.right * cubeDiameter/2f + transform.forward * cubeDiameter/2 + -transform.up * cubeDiameter/2f + transform.up;
        downRight = transform.position + transform.right * cubeDiameter/2f + -transform.forward * cubeDiameter/2 + -transform.up * cubeDiameter/2f + transform.up;
        downLeft = transform.position + -transform.right * cubeDiameter/2f + -transform.forward * cubeDiameter/2 + -transform.up * cubeDiameter/2f + transform.up;
        
        Ray upRightRay = new Ray(upRight, Vector3.down);
        Ray upLeftRay = new Ray(upLeft, Vector3.down);
        Ray downRightRay = new Ray(downRight, Vector3.down);
        Ray downLeftRay = new Ray(downLeft, Vector3.down);

        RaycastHit hit;
        if (Physics.Raycast(upRightRay, out hit, 100, layer))
        {
            upRightHitCoord = hit.point;
        }
        if (Physics.Raycast(upLeftRay, out hit, 100, layer))
        {
            upLeftHitCoord = hit.point;
        }
        if (Physics.Raycast(downRightRay, out hit, 100, layer))
        {
            downRightHitCoord = hit.point;
        }
        if (Physics.Raycast(downLeftRay, out hit, 100, layer))
        {
            downLeftHitCoord = hit.point;
        }

        lowestYPoint = upRightHitCoord.y;
        if (upLeftHitCoord.y < lowestYPoint)
            lowestYPoint = upLeftHitCoord.y;
        if (downRightHitCoord.y < lowestYPoint)
            lowestYPoint = downRightHitCoord.y;
        if (downLeftHitCoord.y < lowestYPoint)
            lowestYPoint = downLeftHitCoord.y;
        
        highestYPoint = upRightHitCoord.y;
        if (upLeftHitCoord.y > highestYPoint)
            highestYPoint = upLeftHitCoord.y;
        if (downRightHitCoord.y > highestYPoint)
            highestYPoint = downRightHitCoord.y;
        if (downLeftHitCoord.y > highestYPoint)
            highestYPoint = downLeftHitCoord.y;


        if (highestYPoint - lowestYPoint > maxHeightOnEdge && highestYPoint - lowestYPoint < 1)
        {
            endYCoord = highestYPoint + .5f;
        }
        else if (highestYPoint - lowestYPoint < 1.5f && highestYPoint - lowestYPoint >= 1)
        {
            endYCoord = highestYPoint - .5f;
        }
        else if (highestYPoint - lowestYPoint > maxHeightOnEdge)
        {
            endYCoord = highestYPoint + .5f;
        }
        else
        {
            endYCoord = lowestYPoint + .5f;
        }

        canStartMoving = true;
    }

    public void Update()
    {
        if (canStartMoving)
        {
            if (transform.position.y >  endYCoord)
            {
                speed += -Physics.gravity.y * Time.deltaTime;
                
                if (speed > terminalSpeed)
                    speed = terminalSpeed;

                transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
            }
        }
        
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(upRight, upRightHitCoord);
        Gizmos.DrawLine(upLeft, upLeftHitCoord);
        Gizmos.DrawLine(downRight, downRightHitCoord);
        Gizmos.DrawLine(downLeft, downLeftHitCoord);
    }
}
