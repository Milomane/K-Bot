using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    [SerializeField] private CinemachineFreeLook cinemachineFreeLookCamera;
    public Animator animator;
    public PlayerMovement playerMovement;
    
    private bool lockCamera;
    private bool lockMovement;
    
    public bool showSelectorMenu;
    public bool stopMovement;
    public bool brutStopMovement;
    public bool controlCursor;

    public bool dying = false;

    private bool grabbing = false;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            grabbing = !grabbing;
        
        animator.SetBool("Grab", grabbing);
        
        playerMovement.stopMovement = stopMovement;
        playerMovement.brutStopMovement = brutStopMovement;
        
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
