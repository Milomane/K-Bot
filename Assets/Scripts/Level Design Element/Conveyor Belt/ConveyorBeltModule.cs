using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltModule : MonoBehaviour
{
    public float speed;

    public Transform endPoint;

    public List<GameObject> onBelt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= onBelt.Count -1; i++)
        {
            onBelt[i].transform.position = Vector3.MoveTowards(onBelt[i].transform.position, endPoint.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onBelt.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        onBelt.Remove(other.gameObject);
    }
}
