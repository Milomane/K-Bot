using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupPowerUp : MonoBehaviour
{
    public GameObject player;

    public int powerUpIndex;

    private bool _stopDo;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void EndAnim()
    {
        player.GetComponent<PlayerDeathHandler>().NewPowerUp(powerUpIndex);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButton("Submit") && _stopDo == false)
        {
            animator.Play("PowerUpRecup");
            _stopDo = true;
        }
    }
}
