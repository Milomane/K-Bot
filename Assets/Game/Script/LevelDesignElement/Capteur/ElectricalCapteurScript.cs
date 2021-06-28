using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalCapteurScript : MonoBehaviour
{
    public int alimOn;
    private bool moduleConnect;
    public GameObject[] light;
    public Material blue, red;

    public UnityEvent alimEvent;

    public UnityEvent shutDownEvent;
    // Start is called before the first frame update
    void Start()
    {
        moduleConnect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alimOn > 0 && !moduleConnect)
        {
            alimEvent.Invoke();
            light[0].GetComponent<MeshRenderer>().material = blue;
            light[1].GetComponent<MeshRenderer>().material = blue;
            light[2].GetComponent<MeshRenderer>().material = blue;
            light[3].GetComponent<MeshRenderer>().material = blue;
            light[4].GetComponent<MeshRenderer>().material = blue;
            moduleConnect = true;
        }
        if (alimOn <= 0 && moduleConnect)
        {
            shutDownEvent.Invoke();
            light[0].GetComponent<MeshRenderer>().material = red;
            light[1].GetComponent<MeshRenderer>().material = red;
            light[2].GetComponent<MeshRenderer>().material = red;
            light[3].GetComponent<MeshRenderer>().material = red;
            light[4].GetComponent<MeshRenderer>().material = red;
            moduleConnect = false;
        }
    }
}
