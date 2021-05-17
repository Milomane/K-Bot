using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorModule : MonoBehaviour
{
    [SerializeField]
    private float speed;
    
    public Transform endPoint;
    public GameObject goInitialPosition;
    
    public bool doorActivation;
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
            transform.position = Vector3.MoveTowards(transform.position, goInitialPosition.transform.position, speed * Time.deltaTime);
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
}
