using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RiverMove : MonoBehaviour
{
    public bool x, z, mx, mz, mxz, xmz, mxmz, xz;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if (speed <= 1)
        {
            speed = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerDeathHandler>().StartDeath(PlayerDeathHandler.DeathType.normal);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (x)
        {
            other.transform.Translate(Vector3.right * speed * Time.fixedDeltaTime, Space.World);
        }
        else if (mx)
        {
            other.transform.Translate(-Vector3.right * speed * Time.fixedDeltaTime, Space.World);
        }
        else if (mz)
        {
            other.transform.Translate(-Vector3.forward * speed * Time.fixedDeltaTime, Space.World);
        }
        else if (mxmz)
        {
            Vector3 mxmzVector = new Vector3(-0.5f,0,-0.5f);
            other.transform.Translate(mxmzVector * speed * Time.fixedDeltaTime, Space.World);
        }
        else if (xz)
        {
            Vector3 xzVector = new Vector3(0.5f,0,0.5f);
            other.transform.Translate(xzVector * speed * Time.fixedDeltaTime, Space.World);
        }
        else if (z)
        {
            other.transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.World);
        }
        else if (mxz)
        {
            Vector3 mxzVector = new Vector3(-0.5f,0,0.5f);
            other.transform.Translate(mxzVector * speed * Time.fixedDeltaTime, Space.World);
        }
        else if (xmz)
        {
            Vector3 xmzVector = new Vector3(0.5f,0,-0.5f);
            other.transform.Translate(xmzVector * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}
