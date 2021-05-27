using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float cubeDiameter = 1f;
    
    public float springForce = 10;
    public Animator testAnimator;

    private Vector3 upRight;
    private Vector3 upLeft;
    private Vector3 downRight;
    private Vector3 downLeft;
    
    private Vector3 upRightHitCoord;
    private Vector3 upLeftHitCoord;
    private Vector3 downRightHitCoord;
    private Vector3 downLeftHitCoord;


    public void UseSpring()
    {
        testAnimator.SetTrigger("Use");
    }

    public void CheckHeight()
    {
        RaycastHit hit;
    }
}
