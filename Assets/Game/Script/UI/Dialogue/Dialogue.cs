using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dialogue : MonoBehaviour
{
    [TextArea(5,10)]
    public string[] dialogue;
    [SerializeField]  private int _actualLine;
    public float speed;
    private GameObject player;
    public float distNeed;
    public bool running;
    private bool dialogueStarted;
    [SerializeField] private bool keepIdle;
    public Animator anim;

    public AudioClip[] bipSounds;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance.gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = PlayerController.instance.gameObject;
        
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if ( dist <= distNeed || dialogueStarted) // verify if the player is close enogh to talk to a pnj
        {
            if (Input.GetButtonDown("Interaction") && running == false) 
            {
                anim.SetBool("Idle",true);
                if (_actualLine <= dialogue.Length-1) // change the text line
                {
                    dialogueStarted = true;
                    PlayerController.instance.stopMovement = true;
                    NextLine();
                    CanvasEventManager.instance.dialogueLookPlayer.gameObject.SetActive(true);
                    CanvasEventManager.instance.dialogueLookPlayer.SetTarget(gameObject);
                }
                else // close text box
                {
                    dialogueStarted = false;
                    PlayerController.instance.stopMovement = false;
                    StopCoroutine(TypeDialog());
                    CanvasEventManager.instance.npcText.text = null;
                    _actualLine = 0;
                    running = false;
                    CanvasEventManager.instance.dialogueLookPlayer.gameObject.SetActive(false);
                    if (keepIdle == false)
                    {
                        anim.SetBool("Idle", false);
                    }
                }
            }
        }
    }
    IEnumerator TypeDialog() // type text character by character
    {
        running = true;
        foreach (char c in dialogue[_actualLine].ToCharArray())
        {
            CanvasEventManager.instance.npcText.text += c;
            audioSource.clip = bipSounds[Random.Range(0, bipSounds.Length)];
            audioSource.Play();
            yield return new WaitForSeconds(speed);
        }
        yield return 
        running = false;
    }
    void NextLine()
    {
        if (_actualLine <= dialogue.Length-1)
        {
            CanvasEventManager.instance.npcText.text = null;
            StartCoroutine(TypeDialog());
            _actualLine++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distNeed);
    }
}
