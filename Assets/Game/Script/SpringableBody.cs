using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpringableBody : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool alignWithWorldUp;
    private Vector2 targetDir;
    private Quaternion targetRotation;
    private Vector3 springPos;

    [Header("Cube as platform")] 
    [SerializeField] private float distanceAboveCube = .4f;
    [SerializeField] private float rayRadius = .35f;
    private bool playerOnRb;

    private bool playerDetection;
    private Transform playerGroup = null;
    private Transform player = null;
    
    void Start()
    {
        targetDir = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        targetRotation = Quaternion.LookRotation(new Vector3(targetDir.x, 0, targetDir.y));
    }
    void Update()
    {
        if (alignWithWorldUp)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1);
            transform.position = Vector3.Lerp(transform.position, new Vector3(springPos.x, transform.position.y, springPos.z), .4f);
        }

        if (springPos != Vector3.zero)
        {
            bool playerHere = false;
            if (Mathf.Abs(transform.position.x - springPos.x) < .1f &&
                Mathf.Abs(transform.position.z - springPos.z) < .1f)
            {
                RaycastHit[] hits;
                hits = Physics.BoxCastAll(transform.position + Vector3.up * distanceAboveCube,
                    new Vector3(rayRadius, rayRadius, rayRadius), targetDir);
                foreach (var hit in hits)
                {
                    if (hit.collider.tag == "Player")
                    {
                        if (!playerOnRb)
                        {
                            PlayerEnterCast(hit.collider.gameObject);
                        }
                            
                        playerHere = true;
                    }
                }
            }

            if (!playerHere && playerOnRb)
            {
                Debug.Log("Quit platform");
            }
            playerOnRb = playerHere;
        }
    }
    
    private void PlayerEnterCast(GameObject player)
    {
        player = player;
        playerDetection = true;
        playerGroup = player.transform.parent;
        playerGroup.transform.parent = transform;
    }

    private void PlayerExitCast(GameObject player)
    {
        playerDetection = false;
        playerGroup.transform.parent = null;
        player = null;
        playerGroup = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Spring" && other.contacts[0].normal.y > .4f && other.contacts[0].normal.x < .5f && other.contacts[0].normal.z < .5f)
        {
            alignWithWorldUp = true;
            
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            
            rb.AddForce(other.collider.GetComponent<Spring>().springForce * Vector3.up * 35);

            springPos = other.collider.transform.position;
            
            other.collider.GetComponent<Spring>().UseSpring();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * distanceAboveCube, new Vector3(rayRadius, rayRadius, rayRadius));
    }
}
