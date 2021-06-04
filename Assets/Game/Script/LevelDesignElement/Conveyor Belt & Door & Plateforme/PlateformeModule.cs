using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeModule : MonoBehaviour
{
    public bool plateformeActivation;
    public bool automatic;
    public bool playerDetection, blocageBool;

    public float speed;
    public float pointHeight, valueHeight, value;
    public Vector3[] points;
    public int pointNumber;
    [SerializeField]
    private Vector3 currentTarget;
    private Vector3 heading;
    

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

        tolerance = speed * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (blocageBool)
        {
            if (playerDetection)
            {
                valueHeight -= value * Time.deltaTime;
                currentTarget.y = valueHeight;
                if (currentTarget.y <= 0.5f)
                {
                    currentTarget.y = 0.5f;
                }
            }
            else if (!playerDetection)
            {
                currentTarget.y = pointHeight;
            } 
        }
        
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
        heading = currentTarget - transform.position;
        
        transform.position += (heading / heading.magnitude) * speed * Time.fixedDeltaTime;

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
            playerDetection = true;
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
            playerDetection = false;
            playerGroup.transform.parent = null;
            player = null;
            playerGroup = null;
        }
    }
}
