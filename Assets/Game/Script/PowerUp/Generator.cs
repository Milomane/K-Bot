using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public float radius = 10f;
    public Transform sphereRangeTransform;
    private GeneratorSphereRange generatorSphere;

    public void Start()
    {
        sphereRangeTransform.localScale = new Vector3(radius, radius, radius);
        generatorSphere = sphereRangeTransform.gameObject.AddComponent<GeneratorSphereRange>();
    }

    public void UnlinkAtDeath()
    {
        generatorSphere.AtDestruction();
    }
}
