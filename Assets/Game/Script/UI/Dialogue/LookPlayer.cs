using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LookPlayer : MonoBehaviour
{
    public Camera cam;

    public Vector3 test;

    public GameObject target;


    public void Awake()
    {
        cam = Camera.main;
    }

    
    void Update()
    {
        transform.position = cam.WorldToScreenPoint(target.transform.position + Vector3.up * 3) + test;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        transform.position = cam.WorldToScreenPoint(target.transform.position + Vector3.up * 3) + test;
    }
}
