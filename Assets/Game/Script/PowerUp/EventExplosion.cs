using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExplosion : MonoBehaviour
{
    [SerializeField] private LayerMask breakableLayer;
    [SerializeField] private float explosionRadius;
    
    void Start()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.forward, explosionRadius, breakableLayer);
        
        foreach (var hit in hits)
        {
            hit.collider.GetComponent<Breakable>().BreakObject();
        }
        
        
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
