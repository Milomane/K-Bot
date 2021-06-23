using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TMP_Text textBox;
    public string[] dialogue;
    [SerializeField]  private int _actualLine;
    public float speed;
    private GameObject player;
    public float distNeed;
    public bool running;
    [SerializeField] private bool keepIdle;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        textBox = GameObject.FindGameObjectWithTag("TextNpc").GetComponent<TMP_Text>();
        textBox.transform.parent.gameObject.SetActive(false);
        player = PlayerController.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = PlayerController.instance.gameObject;
        
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if ( dist <= distNeed) // verify if the player is close enogh to talk to a pnj
        {
          
            
            if (Input.GetButtonDown("Interaction") && running == false) 
            {
                anim.SetBool("Idle",true);
                GameObject textObj = textBox.gameObject;
                textObj.transform.parent.GetComponent<lookPlayer>().target = gameObject;
                if (_actualLine <= dialogue.Length-1) // change the text line
                {
                    PlayerController.instance.stopMovement = true;
                    NextLine();
                    textBox.transform.parent.gameObject.SetActive(true);
                }
                else // close text box
                {
                    PlayerController.instance.stopMovement = false;
                    StopCoroutine(TypeDialog());
                    textBox.text = null;
                    _actualLine = 0;
                    running = false;
                    textBox.transform.parent.gameObject.SetActive(false);
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
            textBox.text += c;
            yield return new WaitForSeconds(speed);
            
        }
        yield return 
        running = false;
    }
    void NextLine()
    {
        if (_actualLine <= dialogue.Length-1)
        {
            textBox.text = null;
            StartCoroutine(TypeDialog());
            _actualLine++;
        }
    }
}
