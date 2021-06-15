using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorOffset : MonoBehaviour
{
    public bool activationOffset;
    
    // Start is called before the first frame update
    public float scrollSpeed;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer> ();
    }

    void Update()
    {
        if (activationOffset)
        {
            float offset = Time.time * -scrollSpeed;
            rend.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}
