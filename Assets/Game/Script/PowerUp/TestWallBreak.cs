using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWallBreak : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem particle;
    
    public void WallBreak()
    {
        animator.SetBool("Break", true);
        particle.Play();
    }
}
