using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

/// <summary>

/// this script is meant to be outside the camera, 
/// inside a cameraManager empty game object if possible

/// </summary>
public class CameraHandler : MonoBehaviour
{
    [Header("CameraReference")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;


    [Header("Inputs")]
    [SerializeField] InputActionReference zoom;
    [SerializeField] InputActionReference resetZoom;


    [Header("ZoomValues")]
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float fovMin = 13f;
    [SerializeField] float defaultFov = 20f;
    [SerializeField] float fovMax = 33f;

    private float targetFieldOfView = 50f;

    private void OnEnable() 
    {
        zoom.action.Enable();
        resetZoom.action.Enable();
    }

    private void Start() 
    {
        virtualCamera.m_Lens.FieldOfView = defaultFov;
    }

    private void Update() 
    {
        HandleZoom();
        ResetZoom();
    }

    #region ZoomMethods
    private void HandleZoom()
    {
        Vector2 zoomValue = zoom.action.ReadValue<Vector2>();
        float fovIncreaseAmount = 5f;

        if (zoomValue.magnitude != 0)
        {
            if (zoomValue.y > 0)
            {
                targetFieldOfView -= fovIncreaseAmount;
            }

            else if (zoomValue.y < 0)
            {
                targetFieldOfView += fovIncreaseAmount;
            }
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fovMin, fovMax);

        virtualCamera.m_Lens.FieldOfView = 
        Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void ResetZoom()
    {
        if (resetZoom.action.WasPerformedThisFrame())
        {
            if (virtualCamera.m_Lens.FieldOfView != defaultFov)
            {
                virtualCamera.m_Lens.FieldOfView = 
                Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, defaultFov, Time.deltaTime * (zoomSpeed * 4));
            }
        }
    }
    #endregion

    private void OnDisable() 
    {
        zoom.action.Disable();
        resetZoom.action.Disable();
    }
}
