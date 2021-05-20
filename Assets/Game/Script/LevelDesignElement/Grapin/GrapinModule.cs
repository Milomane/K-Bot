using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapinModule : MonoBehaviour
{
    public Transform initialPoint, finalPointZ, finalPointX;
    
    public bool horizontalMoveZ, horizontalMoveX;

    private bool readyMoveZ, readyMoveX;

    public float speedMovement;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPoint.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == initialPoint.position)
        {
            readyMoveX = true;
            readyMoveZ = true;
        }

        if (transform.position == finalPointX.position)
        {
            if (horizontalMoveX)
            {
                horizontalMoveX = false;
            }
            else if (!horizontalMoveX)
            {
                horizontalMoveX = true;
            }
        }
        if (transform.position == finalPointZ.position)
        {
            if (horizontalMoveZ)
            {
                horizontalMoveZ = false;
            }
            else if (!horizontalMoveZ)
            {
                horizontalMoveZ = true;
            }
        }
    }

    public void LeavePlate()
    {
        if (transform.position == initialPoint.position)
        {
            horizontalMoveX = false;
            horizontalMoveZ = false;
        }
    }

    public void MoveXEnter()
    {
        if (readyMoveZ && readyMoveX)
        {
            readyMoveZ = false;
        }
        
        
    }
    
    public void MoveZEnter()
    {
        if (readyMoveX && readyMoveZ)
        {
            readyMoveX = false;
        }
    }

    public void MoveZ()
    {
        if (readyMoveZ)
        {
            if (horizontalMoveZ)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPoint.position,
                    speedMovement * Time.deltaTime);
            }
            else if (!horizontalMoveZ)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, finalPointZ.position,
                    speedMovement * Time.deltaTime);
            }
        }
        
    }
    public void MoveX()
    {
        if (readyMoveX)
        {
            if (horizontalMoveX)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPoint.position,
                    speedMovement * Time.deltaTime);
            }
            else if (!horizontalMoveX)
            {
                transform.position = Vector3.MoveTowards(transform.position, finalPointX.position,
                    speedMovement * Time.deltaTime);
            }
        }
        
    }
}
