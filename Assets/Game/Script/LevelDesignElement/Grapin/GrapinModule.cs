using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapinModule : MonoBehaviour
{
    public Transform initialPoint, finalPointZ, finalPointX;
    public Vector3 upPoint, downPoint;
    
    public bool horizontalMoveZ, horizontalMoveX;

    public bool readyMoveZ, readyMoveX, readyMoveHorizontal, finalVerticalMove, verouillage;

    public float speedMovement;

    public bool DownOn, recupObjet;

    public GameObject objet;

    public Transform parentObjet, initialParent;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPoint.transform.position;
        readyMoveHorizontal = true;
        initialParent = gameObject.transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == upPoint)
        {
            finalVerticalMove = false;
            readyMoveHorizontal = true;
        }

        if (!verouillage)
        {
            if (transform.position == initialPoint.position)
            {
                readyMoveX = true;
                readyMoveZ = true;
            } 
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
        
        if (!DownOn && finalVerticalMove)
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
        gameObject.transform.parent = initialParent.transform;
        upPoint = new Vector3(transform.position.x, initialPoint.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, upPoint,
            speedMovement * Time.deltaTime);
        //grappin close bite
    }

    public void Down()
    {
        gameObject.transform.parent = initialParent.transform;
        downPoint = new Vector3(transform.position.x, initialPoint.position.y -5f, transform.position.z);
        DownOn = true;
        transform.position = Vector3.MoveTowards(transform.position, downPoint,
            speedMovement * Time.deltaTime);
        readyMoveHorizontal = false;
        finalVerticalMove = true;
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
            if (objet.transform.parent != gameObject.transform)
            {
                parentObjet.transform.parent = null;
            }
            else
            {
                objet.transform.parent = null;
            }
            objet.GetComponent<Rigidbody>().useGravity = true;
            objet.GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (recupObjet)
        {
            Debug.Log("collision objet");
            objet = other.gameObject;
            objet.GetComponent<Rigidbody>().useGravity = false;
            objet.GetComponent<Rigidbody>().isKinematic = true;
            if (objet.transform.parent != null)
            {
                parentObjet = objet.transform.parent;
                parentObjet.transform.parent = transform;
            }
            else
            {
                objet.transform.parent = transform;
            }
        }
    }
}
