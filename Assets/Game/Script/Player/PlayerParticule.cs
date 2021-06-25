using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class PlayerParticule : MonoBehaviour
{
    public ParticleSystem particuleGround;

    public ParticleSystem particuleJump;

    public ParticleSystem particuleFall;

    public ParticleSystem particuleSpint;

    public PlayerMovement playerMov;

    public CinemachineFreeLook camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerMov.sprint && playerMov.realSpeed > 4 )
        {
            
            StartCoroutine(Lerp());
        }
        else
        {
          
            particuleSpint.Stop();
            StopAllCoroutines();
            camera.m_Lens.FieldOfView -= 0.5f;
        }

        if ( camera.m_Lens.FieldOfView <= 40)
        {
            camera.m_Lens.FieldOfView = 40;
        }

        if (playerMov.isGrounded == false)
        {
            particuleSpint.Stop();
        }
    }

    public void ParticuleGround()
    {
        if (playerMov.sprint == false)
        {
            particuleGround.Play();
           
        }
        else
        {
           particuleSpint.Play();
        }
        
    }

    public void ParticuleFall()
    {
        particuleFall.Play();
    }

    public void ParticuleJump()
    {
        particuleJump.Play();
    }
    
    IEnumerator Lerp()
    {
        float timeElapsed = 0;
        float lerp = 1;
        while (timeElapsed < lerp)
        {
            camera.m_Lens.FieldOfView = Mathf.Lerp(40, 43, timeElapsed / lerp);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        camera.m_Lens.FieldOfView = 43;
       
    }
    
  
}
