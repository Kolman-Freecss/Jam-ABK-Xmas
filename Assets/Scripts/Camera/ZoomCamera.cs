using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] Camera cameraZoom;
    [SerializeField] float maxFieldOfView = 67f; //porcentaje aximo de zoomm de camara.
    [SerializeField] Scrollbar scrollbar;

    [SerializeField] private InputActionReference zoom;

    private void OnEnable()
    {
        zoom.action.Enable();
    }
    private void Start()
    {
        cameraZoom.fieldOfView = scrollbar.value * maxFieldOfView;
    }

    private void Update()
    {
        ChangeZoom();
    }


    public void ChangeZoom()
    {
        cameraZoom.fieldOfView = scrollbar.value * maxFieldOfView;
    }

    private void OnDisable()
    {
        zoom.action.Disable();
    }
}
