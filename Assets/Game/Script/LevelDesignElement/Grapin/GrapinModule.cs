using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapinModule : MonoBehaviour
{
    // Grapple initial point
    public Transform initialPoint;
    
    // Points (Up and down)
    public Vector3 upPoint, downPoint;

    public bool finalVerticalMove, verouillage;

    public float speedMovement;

    public bool DownOn, recupObjet;

    public GameObject objet, barreOn, barreTwo, machine;

    public Transform parentObjet, initialParent;
    public Transform x, mX, z, mZ;

    public Vector3 inputNormalized;

    public bool invertHorizontal;
    public bool invertVertical;

    private bool isGrappleJustStop;
    private bool canDoSound;
    
    private Vector3 lastUpdatePosition = Vector3.zero;
    private Vector3 distance;
    private float currentSpeed;

    // Audio source
    private AudioSource audioSource;

    // Audio clip
    [SerializeField] private AudioClip grappleMove;
    [SerializeField] private AudioClip grappleStop;
        
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialParent = gameObject.transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if no null
        if (objet != null)
        {
            objet.transform.position = new Vector3(transform.position.x, objet.transform.position.y, transform.position.z);
        }
        
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
            
            machine.transform.position = new Vector3(transform.position.x -1.059f, machine.transform.position.y,transform.position.z-0.082f);
        }
        
        if (inputNormalized != Vector3.zero)
        {
            gameObject.transform.Translate(inputNormalized * speedMovement * Time.fixedDeltaTime);

            canDoSound = true;
        }
        else
        {
            canDoSound = false;
        }
        
        // Check if grapple is moving
        distance = transform.position - lastUpdatePosition;
        currentSpeed = distance.magnitude / Time.deltaTime;
        lastUpdatePosition = transform.position;

        if (currentSpeed > 0f && canDoSound)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = grappleMove;
                audioSource.Play();
                isGrappleJustStop = true;
            }
        }
        else
        {
            if (isGrappleJustStop)
            {
                AudioSource.PlayClipAtPoint(grappleStop, transform.position);
                isGrappleJustStop = false;
            }
            audioSource.Stop();
        }

        if (!DownOn && finalVerticalMove)
        {
            Up();
        }
    }

    public void InputMove(float horizontal, float vertical)
    {
        float hor = 1f;
        if (invertHorizontal)
            hor = -1;
        float vert = 1f;
        if (invertVertical)
            vert = -1;

        inputNormalized = new Vector3(horizontal * hor, 0f, vertical * vert).normalized;
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
        downPoint = new Vector3(transform.position.x, initialPoint.position.y - 5f, transform.position.z);
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
