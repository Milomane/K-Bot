using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LookPlayer : MonoBehaviour
{
    public Camera cam;

    public Vector3 test;
    public float upBorder;
    public float downBorder;
    public float leftBorder;
    public float rightBorder;
    

    public GameObject target;


    public void Awake()
    {
        cam = Camera.main;
    }

    
    void Update()
    {
        transform.position = cam.WorldToScreenPoint(target.transform.position + Vector3.up * 3) + test;
        if (transform.position.y > upBorder)
            transform.position = new Vector3(transform.position.x, upBorder, transform.position.z);
        if (transform.position.y < downBorder)
            transform.position = new Vector3(transform.position.x, downBorder, transform.position.z);
        if (transform.position.x < leftBorder)
            transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
        if (transform.position.x > rightBorder)
            transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        transform.position = cam.WorldToScreenPoint(target.transform.position + Vector3.up * 3) + test;
    }
}
