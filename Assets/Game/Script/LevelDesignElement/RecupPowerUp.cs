using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupPowerUp : MonoBehaviour
{
    public GameObject player;

    public int powerUpIndex;

    private bool _stopDo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetButton("Submit") && _stopDo == false)
        {
            player.GetComponent<PlayerDeathHandler>().NewPowerUp(powerUpIndex);
            _stopDo = true;
        }
        
    }
}
