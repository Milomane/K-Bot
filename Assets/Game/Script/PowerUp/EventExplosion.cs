using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExplosion : MonoBehaviour
{
    [SerializeField] private LayerMask breakableLayer;
    [SerializeField] private LayerMask cannonLayer;
    [SerializeField] private float explosionRadius;
    
    void Start()
    {
        // Hits declaration
        RaycastHit[] hits;
        RaycastHit[] hitsCannon;
        
        // Get hits
        hits = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.forward, explosionRadius, breakableLayer);
        hitsCannon = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.forward, explosionRadius, cannonLayer);
        
        // Break objects
        foreach (var hit in hits)
        {
            hit.collider.GetComponent<Breakable>().BreakObject();
        }
        
        // Fire cannon
        foreach (var hit in hitsCannon)
        {
            hit.collider.GetComponent<FactoryCannon>().Fire();
            
        }
        
        
        
        
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
