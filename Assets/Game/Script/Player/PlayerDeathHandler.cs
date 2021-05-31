using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Cinemachine;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerDeathHandler : MonoBehaviour
{
    public static PlayerDeathHandler instance;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;

    [SerializeField] private Death[] deaths;

    [SerializeField] private int maxBody = 3;
    public static DeathType selectedDeath = DeathType.normal;
    private TextMeshProUGUI nbBodiesAvailable;

    public GameObject repairStation;
    public bool canDie = true;

    public bool dieing;
    public Queue<GameObject> bodys;

    private DeathType previousSelectedDeath;
    private Rigidbody rbToStop;
   


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
        nbBodiesAvailable = GameObject.FindWithTag("NbBodiesTxt").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (nbBodiesAvailable != null)
        {
            nbBodiesAvailable.text = (maxBody - bodys.Count).ToString();
        }
        
        if (Input.GetButtonDown("Kill") && canDie && !dieing)
        {
            StartDeath(selectedDeath);
        }

        if (Input.GetButtonDown("ResetRobot") && !dieing)
        {
            DestroyAllBody();
        }

        if (previousSelectedDeath != selectedDeath)
        {
            previousSelectedDeath = selectedDeath;
            ChangeBackModel();
        }
    }

    public void StartDeath(DeathType deathType)
    {
        StartCoroutine(DeathEnumerator(deathType));
    }

    public IEnumerator DeathEnumerator(DeathType deathType)
    {
        dieing = true;
        playerController.stopMovement = true;
        
        // Animation player
        
        // Wait for the player animation to end
        yield return new WaitForSeconds(1);
        playerController.brutStopMovement = true;

        GameObject eventObject = null;
        GameObject particleObject = null;
        
        // Instantiate what's handle the effect after death
        if (deaths[(int)deathType].eventPrefab != null)
            eventObject = Instantiate(deaths[(int)deathType].eventPrefab, transform.position, transform.rotation);
        if (deaths[(int)deathType].particlePrefab != null)
            particleObject = Instantiate(deaths[(int)deathType].particlePrefab, transform.position, transform.rotation);

        float maxRbAngular = 1;
        
        // Switch for special event if needed
        switch (deathType)
        {
            case DeathType.normal:
                bodys.Enqueue(eventObject);
                maxRbAngular = eventObject.GetComponent<EventDeath>().body.GetComponent<Rigidbody>().maxAngularVelocity;
                eventObject.GetComponent<EventDeath>().body.GetComponent<Rigidbody>().maxAngularVelocity = 0;
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
                break;
            case DeathType.crunshed:
                break;
            default :
                Debug.LogError("Error in StartDeath, wrong value for death");
                yield break;
        }
        
        yield return new WaitForSeconds(.1f);
        switch (deathType)
        {
            case DeathType.normal:
                eventObject.GetComponent<EventDeath>().body.GetComponent<Rigidbody>().maxAngularVelocity = maxRbAngular;
                break;
            case DeathType.explosion:
                break;
            case DeathType.spring:
                break;
            case DeathType.generator:
                break;
            case DeathType.lamp:
                break;
            case DeathType.accelerator:
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
        playerModel.model.SetActive(false);
        
        // Wait a bit to let the player see what's happening
        yield return new WaitForSeconds(2);
        
        // Teleport player camera back
        GetComponent<CharacterController>().enabled = false;
        transform.position = repairStation.transform.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, repairStation.transform.eulerAngles.y, transform.eulerAngles.z);
        cinemachineFreeLook.m_XAxis.Value = repairStation.transform.eulerAngles.y - 90;
        repairStation.GetComponent<Animator>().SetBool("Open", false);
        GetComponent<CharacterController>().enabled = true;
        

        // Make player visible again
        yield return null;
        yield return new WaitForSeconds(repairStation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        playerModel.model.SetActive(true);
        repairStation.GetComponent<Animator>().SetBool("Open", true);
        
        // Wait for rebuild animation to end
        yield return null;
        yield return new WaitForSeconds(repairStation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        
        // Retake control
        cinemachineFreeLook.m_XAxis.Value = repairStation.transform.eulerAngles.y;
        
        playerController.stopMovement = false;
        playerController.brutStopMovement = false;
        dieing = false;
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
                selectedDeath = DeathType.normal;
                Debug.Log("Normal PowerUp Added");
                break;
            case 1:
                selectedDeath = DeathType.explosion;
                Debug.Log("Explosion PowerUp Added");
                break;
            case 2:
                selectedDeath = DeathType.spring;
                Debug.Log("Spring PowerUp Added");
                break;
            case 3:
                selectedDeath = DeathType.generator;
                Debug.Log("Generator PowerUp Added");
                break;
            case 4:
                selectedDeath = DeathType.lamp;
                Debug.Log("Lamp PowerUp Added");
                break;
            case 5:
                selectedDeath = DeathType.accelerator;
                Debug.Log("Accelerator PowerUp Added");
                break;
            default:
                Debug.LogError("Error in change power up, not a valid ID");
                return;
        }
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

    void ChangeBackModel()
    {
        for (int i = 0; i < 6; i++)
        {
            playerModel.arrayBackModels[i].SetActive(i == (int)selectedDeath);
        }
    }
}
