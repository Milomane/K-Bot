using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RepairStation : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color activeColor;

    [ColorUsage(true, true)] 
    [SerializeField] private Color deactiveColor;
    
    [SerializeField] private Renderer[] modelRenderers;
    public static UnityEvent resetRepairStation;

    [SerializeField] private AudioSource audioSource;

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
                modelRenderer.material.SetColor("_EmissionColor", activeColor);
            }
        }
        else
        {
            foreach (var modelRenderer in modelRenderers)
            {
                modelRenderer.material.SetColor("_EmissionColor", deactiveColor);
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
            if (!activeStation)
            {
                other.GetComponent<PlayerDeathHandler>().animatorBlink.SetTrigger("Blink");
                audioSource.Play();
            }
            
            activeStation = true;
            secureTime = 1f;
            resetRepairStation.Invoke();
            resetRepairStation.AddListener(ResetStation);
        }
    }
}
