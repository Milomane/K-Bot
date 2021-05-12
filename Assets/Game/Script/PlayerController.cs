using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool cameraCanMove;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        
    }
}
