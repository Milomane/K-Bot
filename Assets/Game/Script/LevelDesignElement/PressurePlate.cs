using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public GameObject lockedObject;
    public List<GameObject> onPlate = new List<GameObject>();
    public int limit;
    public bool defineOpen;
    public UnityEvent openSystem, closeSystem;
    public bool verouillage;
    public LayerMask transparentForCamera;

    private bool playerOnPlate;
    private bool playerInAir;
    
    // Audio source
    private AudioSource audioSource;
    private bool safe;

    // Audio clip
    [SerializeField] private AudioClip pressureOn;
    [SerializeField] private AudioClip pressureOff;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 0.7f;
    }

    // Update is called once per frame 
    void Update()
    {
        if (PlayerDeathHandler.instance.dying && !safe ) // remove player when suicide
        {
            onPlate.Remove(PlayerDeathHandler.instance.gameObject);
            playerOnPlate = false;
            StartCoroutine(SafePlayerRemove());
        }

        for (int i = 0; i < onPlate.Count; i++) // remove destroy object from list
        {
            if (onPlate[i] == null)
            {
                onPlate.Remove(onPlate[i]);
                Debug.Log("R");
            }  
        }

        playerInAir = !PlayerController.instance.playerMovement.isGrounded;
        int playerCount = 0;
        if (playerInAir && playerOnPlate)
            playerCount = -1;
        

        if (onPlate.Count + playerCount < limit && verouillage) // close door
        {
            //lockedObject.GetComponent<LockedDoor>().Close();
            closeSystem.Invoke();
            verouillage = false;
        }

        if (onPlate.Count + playerCount >= limit && !verouillage) // open door
        {
            //lockedObject.GetComponent<LockedDoor>().Open();
            openSystem.Invoke();
            verouillage = true;
        }
    }

    IEnumerator SafePlayerRemove()
    {
        safe = true;

        yield return  new WaitForSeconds(1f);
        onPlate.Remove(PlayerDeathHandler.instance.gameObject);
        playerOnPlate = false;
        Debug.Log("Z");
        safe = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
        if (other.CompareTag("Player") || other.CompareTag("Corpse") || other.gameObject.CompareTag("Lamp"))
        {
            Debug.Log(other);
            audioSource.PlayOneShot(pressureOn, 1f); 
            onPlate.Add(other.gameObject);
        }
            
        if (other.CompareTag("Player"))
            playerOnPlate = true;
    }
    

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("g");
        onPlate.Remove(other.gameObject);
        if (other.CompareTag("Player"))
        {
            playerOnPlate = false;
            audioSource.PlayOneShot(pressureOff, 1f);
        }
            
    }
}
