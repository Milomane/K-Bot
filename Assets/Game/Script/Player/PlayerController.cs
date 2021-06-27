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

    public int statueCount;
    public int maxStatue = 7;

    private bool grabbing = false;

    public CinemachineVirtualCamera[] cinemachineVirtualCamerasAdded;

    public Material goldMaterial;
    public Renderer[] goldMeshRenderer;

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

    public void SwitchSceneCamera(int cameraId)
    {
        StartCoroutine(SwitchCamera(cameraId));
    }

    public IEnumerator SwitchCamera(int cameraId)
    {
        if (cameraId >= cinemachineVirtualCamerasAdded.Length)
        {
            Debug.LogError("CameraID not valid");
            yield break;
        }
        
        cinemachineFreeLookCamera.enabled = false;
        cinemachineVirtualCamerasAdded[cameraId].enabled = true;
        
        lockCamera = true;
        lockMovement = true;
        
        yield return new WaitForSeconds(4f);

        cinemachineVirtualCamerasAdded[cameraId].enabled = true;
        cinemachineFreeLookCamera.enabled = true;
        
        yield return new WaitForSeconds(1f);

        lockCamera = false;
        lockMovement = false;
    }

    public void IncrementStatue()
    {
        statueCount++;
        if (statueCount >= maxStatue)
        {
            // Transform to gold
            foreach (var rend in goldMeshRenderer)
            {
                rend.material = goldMaterial;
            }
        }
        CanvasEventManager.instance.UpdateStatueCount(statueCount);
    }
}
