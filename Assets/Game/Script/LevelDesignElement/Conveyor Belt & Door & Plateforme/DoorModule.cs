using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorModule : MonoBehaviour
{
    [SerializeField] private float speed;

    public Transform endPoint;
    public GameObject goInitialPosition;

    public bool doorActivation;

    public int crushValueToKill = 2;

    public GameObject test;

    private bool canDoOpenSound = true;
    private bool canDoCloseSound;
    private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;

    public MeshRenderer renderer;
    
    [ColorUsage(true, true)]
    public Color activeColor;
    [ColorUsage(true, true)]
    public Color deactiveColor;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        goInitialPosition = new GameObject();
        goInitialPosition.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorActivation)
        {
            renderer.material.SetColor("_EmissionColor", activeColor);
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            if (canDoOpenSound)
            {
                audioSource.PlayOneShot(doorOpen);
                canDoOpenSound = false;
                canDoCloseSound = true;
            }
            
        }
        else
        {
            renderer.material.SetColor("_EmissionColor", deactiveColor);
            transform.position = Vector3.MoveTowards(transform.position, goInitialPosition.transform.position,
                speed * Time.deltaTime);
            if (canDoCloseSound)
            {
                audioSource.PlayOneShot(doorClose);
                canDoCloseSound = false;
                canDoOpenSound = true;
            }
        }
    }

    public void DesactivationBoard()
    {
        doorActivation = false;
    }

    public void ActivationBoard()
    {
        doorActivation = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !doorActivation)
        {
            PlayerDeathHandler.instance.IncrementCrushCounter(crushValueToKill);
        }

        if (other.CompareTag("Corpse") && !doorActivation)
        {
            Debug.Log("testing");
            GameObject parentToDestroy = other.transform.parent.gameObject;
            if (doorActivation == false)
            {
                PlayerDeathHandler.instance.IncrementBodyCounter(crushValueToKill, parentToDestroy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerDeathHandler.instance.DecrementCrushCounter();
        }

        if (other.CompareTag("Corpse"))
        {
           
            Debug.Log("moretet");
        }
    }
}
