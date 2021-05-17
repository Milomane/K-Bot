using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeModule : MonoBehaviour
{
    public bool plateformeActivation;
    public bool automatic;

    public float speed;
    public Vector3[] points;
    public int pointNumber;
    private Vector3 currentTarget;

    public float tolerance;
    public float delayTime;
    private float delayStart;
    

    public GameObject player;
    public Transform playerGroup;
    // Start is called before the first frame update
    void Start()
    {

        if (points.Length > 0)
        {
            currentTarget = points[0];
        }

        tolerance = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (plateformeActivation)
        {
            
            if (transform.position != currentTarget)
            {
                MovePlatforme();
            }
            else
            {
                UpdateTarget();
            }
        }
        
    }
    
    private void MovePlatforme()
    {
        Vector3 heading = currentTarget - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;

        if (heading.magnitude < tolerance)
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }
    private void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - delayStart > delayTime)
            {
                NextPlatforme();
            }
        }
    }

    private void NextPlatforme()
    {
        pointNumber++;
        if (pointNumber >= points.Length)
        {
            pointNumber = 0;
        }

        currentTarget = points[pointNumber];
    }
    
    
    public void DesactivationBoard()
    {
        plateformeActivation = false;
    }

    public void ActivationBoard()
    {
        plateformeActivation = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("oui");
            player = other.gameObject;
            playerGroup = player.transform.parent;
            playerGroup.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("non");
            playerGroup.transform.parent = null;
            player = null;
            playerGroup = null;
        }
    }
}
