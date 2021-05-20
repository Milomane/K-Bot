using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springForce = 10;
    public Animator testAnimator;


    public void UseSpring()
    {
        testAnimator.SetTrigger("Use");
    }
}
