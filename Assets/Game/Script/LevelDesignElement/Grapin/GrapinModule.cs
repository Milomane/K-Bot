using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapinModule : MonoBehaviour
{
    public Transform initialPoint;
    public Vector3 upPoint, downPoint;

    public bool finalVerticalMove, verouillage;

    public float speedMovement;

    public bool DownOn, recupObjet;

    public GameObject objet, barreOn, barreTwo;

    public Transform parentObjet, initialParent;
    public Transform x, mX, z, mZ;

    public Vector3 inputNormalized;
    // Start is called before the first frame update
    void Start()
    {
        
        
        initialParent = gameObject.transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x >= x.position.x)
        {
            transform.position = new Vector3(x.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= mX.position.x)
        {
            transform.position = new Vector3(mX.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= z.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z.position.z);
        }
        if (transform.position.z <= mZ.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, mZ.position.z);
        }
        if (barreOn != null && barreTwo != null)
        {
            barreOn.transform.position = new Vector3(transform.position.x, barreOn.transform.position.y,
                barreOn.transform.position.z);
            barreTwo.transform.position = new Vector3(barreTwo.transform.position.x, barreTwo.transform.position.y,
                transform.position.z);
        }
        if (inputNormalized != Vector3.zero)
        {
            gameObject.transform.Translate(inputNormalized * speedMovement * Time.fixedDeltaTime);
        }
        if (!DownOn && finalVerticalMove)
        {
            Up();
        }
    }

    public void InputMove(float horizontal, float vertical)
    {
        inputNormalized = new Vector3(horizontal, 0f, vertical).normalized;
        Debug.Log(inputNormalized);
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
