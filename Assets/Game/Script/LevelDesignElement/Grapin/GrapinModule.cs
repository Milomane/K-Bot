using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapinModule : MonoBehaviour
{
    public Transform initialPoint, finalPointZ, finalPointX, upPoint, downPoint;
    
    public bool horizontalMoveZ, horizontalMoveX;

    private bool readyMoveZ, readyMoveX, readyMoveHorizontal;

    public float speedMovement;

    public bool DownOn, recupObjet;

    public GameObject objet;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPoint.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == upPoint.position)
        {
            readyMoveHorizontal = true;
        }
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

        if (!DownOn)
        {
            Up();
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
        if (readyMoveZ && readyMoveHorizontal)
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
        if (readyMoveX && readyMoveHorizontal)
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

    public void Up()
    {
        transform.position = Vector3.MoveTowards(transform.position, upPoint.position,
            speedMovement * Time.deltaTime);
        //grappin close bite
    }

    public void Down()
    {
        DownOn = true;
        transform.position = Vector3.MoveTowards(transform.position, downPoint.position,
            speedMovement * Time.deltaTime);
        readyMoveHorizontal = false;
        //grappin open bite
    }

    public void ObjectRecup()
    {
        recupObjet = true;
        DownOn = false;
    }

    public void DropObject()
    {
        recupObjet = false;
        if (objet != null)
        {
            objet.transform.parent = null;
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (recupObjet)
        {
            objet = other.gameObject;
            objet.transform.parent = transform;

        }
    }
}
