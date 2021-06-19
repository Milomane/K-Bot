using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breakable : MonoBehaviour
{
    public UnityEvent eventToInvoke;
    

    public void BreakObject()
    {
        eventToInvoke.Invoke();
    }
}
