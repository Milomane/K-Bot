using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapinPlaque : MonoBehaviour
{
    public bool grappin, moveX, moveZ;

    public GrapinModule module;
    

    private void OnTriggerStay(Collider other)
    {
        if (module != null)
        {
            if (grappin)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.Down();
                }
            }
            else if (moveX)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.MoveX();
                }
            }
            else if (moveZ)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.MoveZ();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (module != null)
        {
            if (grappin)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.ObjectRecup();
                }
            }
            else if (moveX)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.LeavePlate();
                }
            }
            else if (moveZ)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.LeavePlate();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (module != null)
        {
            if (grappin)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.DropObject();
                }
            }
            else if (moveX)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.MoveXEnter();
                }
            }
            else if (moveZ)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    module.MoveZEnter();
                }
            }
        }
    }
}
