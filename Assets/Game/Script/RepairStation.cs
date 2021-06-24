using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RepairStation : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Renderer[] modelRenderers;
    public static UnityEvent resetRepairStation;

    private bool activeStation;
    private float secureTime;

    public bool lookRight;
    
    
    void Start()
    {
        resetRepairStation = new UnityEvent();
        resetRepairStation.AddListener(ResetStation);
    }
    
    
    void Update()
    {
        ChangeModelColorIfActive();

        secureTime -= Time.deltaTime;
    }

    public void ChangeModelColorIfActive()
    {
        if (activeStation)
        {
            foreach (var modelRenderer in modelRenderers)
            {
                modelRenderer.material.color = activeColor;
            }
        }
        else
        {
            foreach (var modelRenderer in modelRenderers)
            {
                modelRenderer.material.color = Color.white;
            }
        }
    }

    public void ResetStation()
    {
        if (secureTime <= 0)
        {
            activeStation = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerDeathHandler>().repairStation = gameObject;
            other.GetComponent<PlayerDeathHandler>().animatorBlink.SetTrigger("Blink");
            activeStation = true;
            secureTime = 1f;
            resetRepairStation.Invoke();
            resetRepairStation.AddListener(ResetStation);
        }
    }
}
