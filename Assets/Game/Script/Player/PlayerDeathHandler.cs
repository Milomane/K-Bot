using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Cinemachine;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Component = UnityEngine.Component;
using Object = UnityEngine.Object;

public class PlayerDeathHandler : MonoBehaviour
{
    public static PlayerDeathHandler instance;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;

    [SerializeField] private Death[] deaths;

    [SerializeField] private int maxBody = 3;
    // Nb body available
    private int nbBodyAvailable;
    public static DeathType selectedDeath = DeathType.crunshed;
    public List<DeathType> unlockeDeathAtStart;
    private List<DeathType> unlockedDeath;
    private GameObject nbBodiesAvailable;

    public GameObject repairStation;
    public bool canDie = true;

    public bool dying;
    public Queue<GameObject> bodys;

    private DeathType previousSelectedDeath;
    private Rigidbody rbToStop;
    private int crushCounter = 0;
    private int crushBodyCounter = 0;


    public enum DeathType
    {
        normal,
        explosion,
        spring,
        generator,
        lamp,
        accelerator,
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
        nbBodiesAvailable = GameObject.FindWithTag("NbBodies");

        unlockedDeath = new List<DeathType>();
        
        StartCoroutine(Safe());
    }

    IEnumerator Safe()
    {
        foreach (var deathType in unlockeDeathAtStart)
        {
            NewPowerUp((int)deathType);
            
            yield return new WaitForSeconds(.1f);
            CanvasEventManager.instance.UpdateUnlockedDeath(unlockedDeath);
        }
        
        yield return new WaitForSeconds(.05f);
        
        if (unlockeDeathAtStart.Count == 0)
            CanvasEventManager.instance.UpdateUnlockedDeath(unlockedDeath);
    }

    void Update()
    {
        playerController.dying = dying;
        
        nbBodyAvailable = maxBody - bodys.Count;

        if (Input.GetButtonDown("Kill") && canDie && !dying && selectedDeath != DeathType.crunshed)
        {
            StartDeath(selectedDeath);
        }

        if (Input.GetButtonDown("ResetRobot") && !dying)
        {
            DestroyAllBody();
        }

        // Change back model
        if (previousSelectedDeath != selectedDeath)
        {
            StartCoroutine(ChangeBackModel());
        }
    }

    public void StartDeath(DeathType deathType)
    {
        StartCoroutine(DeathEnumerator(deathType));
    }

    public IEnumerator DeathEnumerator(DeathType deathType)
    {
        dying = true;
        playerController.stopMovement = true;

        // Animation player


        playerController.brutStopMovement = true;
        yield return null;
        controller.enabled = false;
        
        GameObject eventObject = null;
        GameObject particleObject = null;

        // Instantiate what's handle the effect after death
        if (deaths[(int) deathType].eventPrefab != null)
            eventObject = Instantiate(deaths[(int) deathType].eventPrefab, transform.position, transform.rotation);
        if (deaths[(int) deathType].particlePrefab != null)
            particleObject = Instantiate(deaths[(int) deathType].particlePrefab, transform.position,
                Quaternion.Euler(-90, 0, 0));

        float maxRbAngular = 1;

        Destroy(particleObject, 10f);

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
            case DeathType.accelerator:
                bodys.Enqueue(eventObject);
                break;
            case DeathType.crunshed:
                break;
            default:
                Debug.LogError("Error in StartDeath, wrong value for death");
                yield break;
        }

        // Destroy other event if needed
        if (bodys.Count > maxBody)
        {
            DestroyOldestBody();
        }

        // Make player invisible
        playerModel.model.SetActive(false);

        // Wait a bit to let the player see what's happening
        yield return new WaitForSeconds(2);

        // Teleport player camera back
        GetComponent<CharacterController>().enabled = false;
        transform.position = repairStation.transform.position;

        float angleOffset = 0f;
        if (repairStation.GetComponent<RepairStation>())
            if (repairStation.GetComponent<RepairStation>().lookRight)
                angleOffset = 180;
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, repairStation.transform.eulerAngles.y + angleOffset,
            transform.eulerAngles.z);
        cinemachineFreeLook.m_XAxis.Value = repairStation.transform.eulerAngles.y + angleOffset;
        repairStation.GetComponent<Animator>().SetBool("Open", false);
        GetComponent<CharacterController>().enabled = true;


        // Make player visible again
        yield return null;
        yield return new WaitForSeconds(repairStation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        playerModel.model.SetActive(true);
        repairStation.GetComponent<Animator>().SetBool("Open", true);
        FindObjectOfType<PlayerSounds>().Respawn();
        // Wait for rebuild animation to end
        yield return null;
        yield return new WaitForSeconds(repairStation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        // Retake control

        controller.enabled = true;
        playerController.stopMovement = false;
        playerController.brutStopMovement = false;
        dying = false;
    }

    private void FixedUpdate()
    {
        if (rbToStop != null)
        {
            rbToStop.angularVelocity = Vector3.zero;
            rbToStop = null;
        }
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
                selectedDeath = DeathType.accelerator;
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

    public void NewPowerUp(int powerUpId)
    {
        switch (powerUpId)
        {
            case 0:
                unlockedDeath.Add(DeathType.normal);
                Debug.Log("Normal PowerUp Added");
                break;
            case 1:
                unlockedDeath.Add(DeathType.explosion);
                Debug.Log("Explosion PowerUp Added");
                break;
            case 2:
                unlockedDeath.Add(DeathType.spring);
                Debug.Log("Spring PowerUp Added");
                break;
            case 3:
                unlockedDeath.Add(DeathType.generator);
                Debug.Log("Generator PowerUp Added");
                break;
            case 4:
                unlockedDeath.Add(DeathType.lamp);
                Debug.Log("Lamp PowerUp Added");
                break;
            case 5:
                unlockedDeath.Add(DeathType.accelerator);
                Debug.Log("Accelerator PowerUp Added");
                break;
            default:
                Debug.LogError("Error in change power up, not a valid ID");
                return;
        }
        CanvasEventManager.instance.UpdateUnlockedDeath(unlockedDeath);

        ChangePowerUp(powerUpId);
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

    public void DestroySelectedBody(GameObject target)
    {
        for (int i = 0; i < bodys.Count; i++)
        {
            GameObject corpse = bodys.Dequeue();

            if (corpse == target)
            {
                corpse.GetComponent<EventDeath>().DestroyBody();
                Debug.Log(bodys.Count);
            }
            else
            {
                bodys.Enqueue(corpse);
            }
        }
    }

    IEnumerator ChangeBackModel()
    {
        for (int i = 0; i < 6; i++)
        {
            playerModel.arrayBackModels[i].SetActive(i == (int) selectedDeath);
        }

        yield return null;

        previousSelectedDeath = selectedDeath;
    }

    public void IncrementCrushCounter(int destroyAtValue)
    {
        crushCounter++;
        if (crushCounter >= destroyAtValue && !dying)
            StartDeath(DeathType.crunshed);
    }

    public void DecrementCrushCounter()
    {
        crushCounter--;
    }

    public void IncrementBodyCounter(int destroyAtValue, GameObject obj)
    {
        crushBodyCounter++;
        Debug.Log(crushBodyCounter);
        Debug.Log(destroyAtValue);
        if (crushBodyCounter >= destroyAtValue)
            DestroySelectedBody(obj);
    }

    public void DecrementBodyCounter()
    {
        crushBodyCounter--;
    }

    public int GetNbBodyAvailable()
    {
        return nbBodyAvailable;
    }
}