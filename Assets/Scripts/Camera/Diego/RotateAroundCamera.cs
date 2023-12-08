using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateAroundCamera : MonoBehaviour
{
    [SerializeField] InputActionReference cameraRotation;
    [SerializeField] InputActionReference enableRotation;

    [SerializeField] float acceleration = 1440f; //grados por segundo al cuadrado
    [SerializeField] float maxSpeed = 720f; //grados por segundo
    [SerializeField] float deceleration = 720f;

    float currentSpeed;

    private void OnEnable() 
    {
        cameraRotation.action.Enable();
        enableRotation.action.Enable();
    }

    private void Update() 
    {
        Vector2 zoomValue = cameraRotation.action.ReadValue<Vector2>();

        if (enableRotation.action.WasPerformedThisFrame())
        {
            if (zoomValue.x > 0)
            {
                currentSpeed -= acceleration * Time.deltaTime;
                ClampMaxSpeed();
            }
            else if (zoomValue.x < 0)
            {
                currentSpeed += acceleration * Time.deltaTime;
                ClampMaxSpeed();
            }
            else
            {
                float oldCurrentSpeed = currentSpeed;
                currentSpeed += deceleration * Time.deltaTime * -Mathf.Sign(currentSpeed);

                if (Mathf.Sign(currentSpeed) != Mathf.Sign(oldCurrentSpeed))
                    currentSpeed = 0f;
            }
            
        }

        Vector3 newEuler = transform.localEulerAngles + Vector3.up * currentSpeed * Time.deltaTime;
        transform.localEulerAngles = newEuler;
    }

    void ClampMaxSpeed()
    {
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        else if (currentSpeed < -maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    private void OnDisable() 
    {
        cameraRotation.action.Disable();
        enableRotation.action.Disable();
    }
}
