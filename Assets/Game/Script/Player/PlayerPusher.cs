using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPusher : MonoBehaviour
{
    public float mass = 3f; // the lower the mass, the higher the impact
    public float hitForce = 2.5f; // impact "force" when hit by rigidbody 
    private Vector3 impact = Vector3.zero; // character momentum 
    private CharacterController character;
 
    void Start()
    {
        character = GetComponent<CharacterController>();
    }
 
    void AddImpact(Vector3 force)
    {
        var dir = force.normalized;
        //dir.y = 0.5f; // add some velocity upwards - it's cooler this way
        impact += dir.normalized * force.magnitude / mass;
    }
 
    void Update()
    {
        if (impact.magnitude > 0.2)
        { 
            // if momentum > 0.2...
            character.Move(impact * Time.deltaTime); // move character
        }
        // impact vanishes to zero over time
        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
    }
 
    void OnControllerColliderHit (ControllerColliderHit hit)
    { 
        // collision adds impact
        if (hit.rigidbody != null)
            AddImpact(hit.rigidbody.velocity * hitForce);
    }
}
