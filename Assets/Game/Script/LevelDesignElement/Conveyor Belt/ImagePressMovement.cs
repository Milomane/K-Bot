using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePressMovement : MonoBehaviour
{
    public Transform initialPoint, endPoint;

    [SerializeField]
    private float speed;

    private bool stateImage;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.18f;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateImage)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                initialPoint.transform.position, speed * Time.deltaTime);
            if (gameObject.transform.position == initialPoint.transform.position)
            {
                stateImage = false;
            }
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                endPoint.transform.position, speed * Time.deltaTime);
            if (gameObject.transform.position == endPoint.transform.position)
            {
                stateImage = true;
            }
        }
    }
}
