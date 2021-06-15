using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorOffset : MonoBehaviour
{
    // Start is called before the first frame update
    float scrollSpeed = 0.5f;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer> ();
    }

    void Update()
    {
        float offset = Time.time * -scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
