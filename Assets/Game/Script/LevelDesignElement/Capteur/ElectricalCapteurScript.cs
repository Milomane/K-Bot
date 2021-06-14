using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalCapteurScript : MonoBehaviour
{
    public bool alimOn;
    private bool moduleConnect;

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
            moduleConnect = true;
        }
        if (!alimOn && moduleConnect)
        {
            shutDownEvent.Invoke();
            moduleConnect = false;
        }
    }
}
