using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TMP_Text text;
    public string[] dialogue;
  [SerializeField]  private int _actualLine ;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

    /*    if (_actualLine >= dialogue.Length )
        {
           
        }*/
        
        if ( dist <= 2 && _actualLine <= dialogue.Length)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                text.gameObject.SetActive(true);
                text.text = dialogue[_actualLine];
                _actualLine++;
            }
        }
        
        else
        {
            text.gameObject.SetActive(false);
            _actualLine = 0;
            text.text = null;
        }
        
    }
}
