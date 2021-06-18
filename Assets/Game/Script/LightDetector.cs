using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightDetector : MonoBehaviour
{
    public GameObject[] lamp;

    
    public float distance;
   

    public UnityEvent eventActive;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        lamp = GameObject.FindGameObjectsWithTag("Lamp");

        if (lamp != null)
        {
            foreach (GameObject obj in lamp)
            {
                float test = Vector3.Distance(transform.position, obj.transform.position);
                if (test <= distance)
                {
                    eventActive.Invoke();
                    
                }
            }
        }
    }
}
