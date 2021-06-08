using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerParticule : MonoBehaviour
{
    public ParticleSystem particuleGround;

    public ParticleSystem particuleJump;

    public ParticleSystem particuleFall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ParticuleGround()
    {
       particuleGround.Play();
    }

    public void ParticuleFall()
    {
        particuleFall.Play();
    }

    public void ParticuleJump()
    {
        particuleJump.Play();
    }
}
