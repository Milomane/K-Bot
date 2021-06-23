using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class lookPlayer : MonoBehaviour
{
    public Camera cam;

    public Vector3 test;

    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.WorldToScreenPoint(target.transform.position + Vector3.up * 3) + test;
    }
}
