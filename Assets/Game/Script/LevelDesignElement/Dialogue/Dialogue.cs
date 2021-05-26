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
    public GameObject player;
    public float distNeed;
    public bool running;
    // Start is called before the first frame update
    void Start()
    {
        textBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if ( dist <= distNeed) // verify if the player is close enogh to talk to a pnj
        {
            if (Input.GetButtonDown("Submit") && running == false) 
            {
                if (_actualLine <= dialogue.Length-1) // change the text line
                {
                    player.GetComponent<PlayerController>().stopMovement = true;
                    NextLine();
                    textBox.gameObject.SetActive(true);
                }
                else // close text box
                {
                    player.GetComponent<PlayerController>().stopMovement = false;
                    StopCoroutine(TypeDialog());
                    textBox.text = null;
                    _actualLine = 0;
                    running = false;
                    textBox.gameObject.SetActive(false);
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
