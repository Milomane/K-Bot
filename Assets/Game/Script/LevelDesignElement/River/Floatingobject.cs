using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatingobject : MonoBehaviour
{
    public float level = 4f;

    public float height = 2f;
    public float bounceDamp = 0.05f;
    public Vector3 buotancyCenterOffset;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 upLift;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        actionPoint = transform.position + transform.TransformDirection(buotancyCenterOffset);
        forceFactor = 1f - ((actionPoint.y - level) / height);

        if (forceFactor > 0f)
        {
            upLift = -Physics.gravity * (forceFactor - rb.velocity.y * bounceDamp);
            rb.AddForceAtPosition(upLift, actionPoint);
        }
    }
}
