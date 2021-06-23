using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juckebox : MonoBehaviour
{
    [SerializeField] private AudioClip changeMusicButton;

    private bool canInteract;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (canInteract && Input.GetButtonDown("Interaction"))
        {
            Debug.Log("Interaction with " + name);
            JuckeBoxManager.instance.ChangeMusic(audioSource);
            AudioSource.PlayClipAtPoint(changeMusicButton, transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}