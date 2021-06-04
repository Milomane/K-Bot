using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorModule : MonoBehaviour
{
    [SerializeField] private float speed;

    public Transform endPoint;
    public GameObject goInitialPosition;

    public bool doorActivation;

    public int crushValueToKill = 2;

    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        goInitialPosition = new GameObject();
        goInitialPosition.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorActivation)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
        }
        else if (!doorActivation)
        {
            transform.position = Vector3.MoveTowards(transform.position, goInitialPosition.transform.position,
                speed * Time.deltaTime);
        }
    }

    public void DesactivationBoard()
    {
        doorActivation = false;
    }

    public void ActivationBoard()
    {
        doorActivation = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerDeathHandler.instance.IncrementCrushCounter(crushValueToKill);
        }

        if (other.CompareTag("Corpse"))
        {
            Debug.Log("testing");
            GameObject parentToDestroy = other.transform.parent.gameObject;
            if (doorActivation == false)
            {
                PlayerDeathHandler.instance.IncrementBodyCounter(crushValueToKill, parentToDestroy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerDeathHandler.instance.DecrementCrushCounter();
        }

        if (other.CompareTag("Corpse"))
        {
           
            Debug.Log("moretet");
        }
    }
}
