using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCollectable : MonoBehaviour
{
    public bool collectable = true;
    public AudioClip collectClip;
    public float volume;
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && collectable)
        {
            PlayerController.instance.IncrementStatue();
            AudioSource.PlayClipAtPoint(collectClip, transform.position, volume);
            Destroy(gameObject);
        }
        
        
    }
}
