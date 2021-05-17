using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cinemachineFreeLookCamera;
    [SerializeField] private PlayerMovement playerMovement;
    
    private bool lockCamera;
    private bool lockMovement;
    
    public bool showSelectorMenu;
    public bool stopMovement;
    public bool controlCursor;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        playerMovement.stopMovement = stopMovement;
        
        MenuUpdate();
        
    }

    private void MenuUpdate()
    {
        bool needToShowMenu = false;
        bool needToLockCamera = false;
        
        // Active the selector if the button is pressed
        CanvasEventManager.instance.selectorActive = Input.GetButton("Selector") || showSelectorMenu;
        if (Input.GetButton("Selector"))
        {
            needToShowMenu = true;
        }

        if (needToShowMenu || showSelectorMenu || controlCursor)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            needToLockCamera = true;
        }
        else
        {
            if (Cursor.lockState == CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (needToLockCamera || lockCamera)
        {
            cinemachineFreeLookCamera.m_YAxis.m_InputAxisName = "";
            cinemachineFreeLookCamera.m_XAxis.m_InputAxisName = "";
            cinemachineFreeLookCamera.m_YAxis.m_InputAxisValue = 0;
            cinemachineFreeLookCamera.m_XAxis.m_InputAxisValue = 0;
        }
        else
        {
            cinemachineFreeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y";
            cinemachineFreeLookCamera.m_XAxis.m_InputAxisName = "Mouse X";
        }
    }
}
