using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EventExplosion : MonoBehaviour
{
    [SerializeField] private LayerMask breakableLayer;
    [SerializeField] private LayerMask cannonLayer;
    [SerializeField] private float explosionRadius;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private float volume = 0.3f;

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
        
        AudioSource.PlayClipAtPoint(explosionClip, gameObject.transform.position, volume);
        
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
