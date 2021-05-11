using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position,transform.forward,Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward,out hit, Mathf.Infinity))
        {
            
            Debug.Log("test");
        }
    }
}
