using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalCapteurScript : MonoBehaviour
{
    public bool alimOn;
    private bool moduleConnect;
    public Material redMat, blueMat;
    public GameObject[] light;

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
        if (alimOn && !moduleConnect)
        {
            alimEvent.Invoke();
            light[0].GetComponent<MeshRenderer>().material = blueMat;
            light[1].GetComponent<MeshRenderer>().material = blueMat;
            light[2].GetComponent<MeshRenderer>().material = blueMat;
            light[3].GetComponent<MeshRenderer>().material = blueMat;
            light[4].GetComponent<MeshRenderer>().material = blueMat;
            
            moduleConnect = true;
        }
        if (!alimOn && moduleConnect)
        {
            shutDownEvent.Invoke();
            light[0].GetComponent<MeshRenderer>().material = redMat;
            light[1].GetComponent<MeshRenderer>().material = redMat;
            light[2].GetComponent<MeshRenderer>().material = redMat;
            light[3].GetComponent<MeshRenderer>().material = redMat;
            light[4].GetComponent<MeshRenderer>().material = redMat;
            
            moduleConnect = false;
        }
    }
}
