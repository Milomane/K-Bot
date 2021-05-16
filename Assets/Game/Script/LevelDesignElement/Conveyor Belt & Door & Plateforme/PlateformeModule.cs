using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeModule : MonoBehaviour
{
    public bool plateformeActivation;

    public float speed;
    public int valueTransform;
    public List<Transform> transformList;

    public Transform nextStep;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        nextStep = transformList[valueTransform];
    }

    // Update is called once per frame
    void Update()
    {
        if (plateformeActivation)
        {
            if (transform.position != nextStep.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextStep.position, speed * Time.deltaTime);
            }
            else if (transform.position == nextStep.position && valueTransform <= transformList.Count -1)
            {
                ValueIncrease();
            }
            else
            {
                valueTransform = 0;
                transform.position = transformList[valueTransform].position;  
            }

        }

        if (transformList[valueTransform] != null)
        {
            nextStep = transformList[valueTransform];
        }

    }

    private void ValueIncrease()
    {
        valueTransform += 1;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("non");
            player = null;
        }
    }
}
