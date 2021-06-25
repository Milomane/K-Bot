using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightDetector : MonoBehaviour
{
    public GameObject[] lamp;

    
    public float distance;
    
    [ColorUsage(true, true)]
    public Color deactivateColor;
    
    [ColorUsage(true, true)]
    public Color activateColor;

    public MeshRenderer renderer;
   

    public UnityEvent eventActive;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        lamp = GameObject.FindGameObjectsWithTag("Lamp");

        bool active = false;

        if (lamp != null)
        {
            foreach (GameObject obj in lamp)
            {
                float test = Vector3.Distance(transform.position, obj.transform.position);
                if (test <= distance)
                {
                    eventActive.Invoke();
                    active = true;
                }
            }
        }

        if (active)
        {
            renderer.material.SetColor("_EmissionColor", activateColor);
        }
        else
        {
            renderer.material.SetColor("_EmissionColor", deactivateColor);
        }
    }
}
