using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFeedbackRepairMovement : MonoBehaviour
{
    private Vector3 repairStationPos;
    public float interpolation = 5;

    private void Start()
    {
        repairStationPos = PlayerDeathHandler.instance.repairStation.transform.position + new Vector3(0, 4f, 0);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, repairStationPos, interpolation * Time.deltaTime);
        if (Vector3.Distance(transform.position, repairStationPos) < .25f)
        {
            Destroy(gameObject);
        }
    }
}
