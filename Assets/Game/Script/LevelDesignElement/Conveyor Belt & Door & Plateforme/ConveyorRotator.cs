using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorRotator : MonoBehaviour
{
    public float speed;

    public Transform endPoint;

    public List<GameObject> onBelt;
    public GameObject player;

    public bool conveyorActivation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (conveyorActivation)
        {
            for (int i = 0; i <= onBelt.Count -1; i++)
            {
                onBelt[i].transform.position = Vector3.MoveTowards(onBelt[i].transform.position, endPoint.position, speed * Time.deltaTime);
            }

            if (player != null)
            {
                player.transform.position =
                    Vector3.MoveTowards(player.transform.position, endPoint.position, speed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("oui");
            player = other.gameObject;
        }
        else
        {
            onBelt.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("non");
            player = null;
        }
        else
        {
            onBelt.Remove(other.gameObject);
        }
    }
}
