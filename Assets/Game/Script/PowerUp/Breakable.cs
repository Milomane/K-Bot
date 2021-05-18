using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breakable : MonoBehaviour
{
    public UnityEvent eventToInvoke;
    

    public void BreakObject()
    {
        Debug.Log("Break object");
        eventToInvoke.Invoke();
    }
}
