using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupPowerUp : MonoBehaviour
{
    public int powerUpIndex;

    private bool _stopDo;

    public Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void EndAnim()
    {
        PlayerDeathHandler.instance.NewPowerUp(powerUpIndex);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButton("Interaction") && _stopDo == false)
        {
            animator.Play("PowerUpRecup");
            _stopDo = true;
        }
    }
}
