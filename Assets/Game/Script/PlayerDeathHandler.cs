using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public static PlayerDeathHandler instance;

    [SerializeField] private Death[] deaths;

    [SerializeField] private int maxBody = 3;
    public static DeathType selectedDeath = DeathType.normal;

    public GameObject model;
    public GameObject repairStation;
    public bool canDie = true;

    private Queue<GameObject> bodys;


    public enum DeathType
    {
        normal,
        explosion,
        spring,
        generator,
        lamp,
        glue,
        crunshed
    }
    
    [System.Serializable]
    public class Death
    {
        public GameObject particlePrefab;
        public GameObject eventPrefab;
    }
    
    void Start()
    {
        instance = this;
        bodys = new Queue<GameObject>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Kill") && canDie)
        {
            StartDeath(selectedDeath);
        }
    }

    public void StartDeath(DeathType deathType)
    {
        StartCoroutine(DeathEnumerator(deathType));
    }

    public IEnumerator DeathEnumerator(DeathType deathType)
    {
        
        
        // Animation player
        model.GetComponent<Renderer>().material.color = Color.magenta;
        
        // Wait for the player animation to end
        yield return new WaitForSeconds(1);

        // Instantiate what's handle the effect after death
        GameObject eventObject = Instantiate(deaths[(int)deathType].eventPrefab, transform.position, quaternion.identity);
        GameObject particleObject = Instantiate(deaths[(int)deathType].particlePrefab, transform.position, quaternion.identity);
        
        // Switch for special event if needed
        switch (deathType)
        {
            case DeathType.normal:
                bodys.Enqueue(eventObject);
                break;
            case DeathType.explosion:
                break;
            case DeathType.spring:
                bodys.Enqueue(eventObject);
                break;
            case DeathType.generator:
                bodys.Enqueue(eventObject);
                break;
            case DeathType.lamp:
                bodys.Enqueue(eventObject);
                break;
            case DeathType.glue:
                break;
            case DeathType.crunshed:
                break;
            default :
                Debug.LogError("Error in StartDeath, wrong value for death");
                yield break;
        }
        
        // Destroy other event if needed
        if (bodys.Count > maxBody)
        {
            DestroyOldestBody();
        }
        
        // Make player invisible
        model.SetActive(false);
        
        // Wait a bit to let the player see what's happening
        yield return new WaitForSeconds(2);
        
        // Teleport player camera back
        GetComponent<CharacterController>().enabled = false;
        transform.position = repairStation.transform.position;
        GetComponent<CharacterController>().enabled = true;
        
        // Make player visible again
        model.SetActive(true);

        // Rebuild animation
        model.GetComponent<Renderer>().material.color = Color.green;

        // Wait for rebuild animation to end
        yield return new WaitForSeconds(1);
        
        // Retake control
        model.GetComponent<Renderer>().material.color = Color.red;
    }

    public static void ChangePowerUp(int powerUpId)
    {
        switch (powerUpId)
        {
            case 0:
                selectedDeath = DeathType.normal;
                break;
            case 1:
                selectedDeath = DeathType.explosion;
                break;
            case 2:
                selectedDeath = DeathType.spring;
                break;
            case 3:
                selectedDeath = DeathType.generator;
                break;
            case 4:
                selectedDeath = DeathType.lamp;
                break;
            case 5:
                selectedDeath = DeathType.glue;
                break;
            case 6:
                selectedDeath = DeathType.crunshed;
                break;
            default:
                Debug.LogError("Error in change power up, not a valid ID");
                return;
        }
        
        CanvasEventManager.instance.deathTypeSelected = selectedDeath;
    }

    public void DestroyOldestBody()
    {
        bodys.Dequeue().GetComponent<EventDeath>().DestroyBody();
    }

    public void DestroyAllBody()
    {
        while (bodys.Count > 0)
        {
            bodys.Dequeue().GetComponent<EventDeath>().DestroyBody();
        }
    }
}
