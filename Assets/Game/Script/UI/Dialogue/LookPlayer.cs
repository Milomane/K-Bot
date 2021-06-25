using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LookPlayer : MonoBehaviour
{
    public Camera cam;

    public Vector3 test;
    public float upBorderOffset;
    public float downBorderOffset;
    public float leftBorderOffset;
    public float rightBorderOffset;

    private RectTransform rectTransform;
    public Canvas canvas;
    public Vector2 canvasScale;
    

    public GameObject target;


    public void Awake()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        transform.position = cam.WorldToScreenPoint(target.transform.position + Vector3.up * 3);
        transform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, 0);

        float xClamped = Mathf.Clamp(rectTransform.localPosition.x, -1920/2 + rectTransform.sizeDelta.x/2 + leftBorderOffset, 1920/2 - rectTransform.sizeDelta.x/2 + rightBorderOffset);
        float yClamped = Mathf.Clamp(rectTransform.localPosition.y, -1080/2 + rectTransform.sizeDelta.y/2 + downBorderOffset, 1080/2 - rectTransform.sizeDelta.y/2 + upBorderOffset);
        
        rectTransform.localPosition = new Vector3(xClamped, yClamped, rectTransform.localPosition.z);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        transform.position = cam.WorldToScreenPoint(target.transform.position + Vector3.up * 3) + test;
    }
}
