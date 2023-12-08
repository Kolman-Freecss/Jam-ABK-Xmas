#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion

public class ChangeCamera : MonoBehaviour
{
    [SerializeField]
    private InputActionReference changeCamera1;

    [SerializeField]
    private InputActionReference changeCamera2;

    public GameObject[] cameraList;

    private void OnEnable()
    {
        changeCamera1.action.Enable();
        changeCamera2.action.Enable();
    }

    private void Start()
    {
        changeCamera1.action.performed += _ => ChangeCamera1();
        changeCamera2.action.performed += _ => ChangeCamera2();
        ChangeCamera1();
    }

    private void ChangeCamera1()
    {
        cameraList[0].gameObject.SetActive(true);
        cameraList[1].gameObject.SetActive(false);
    }

    private void ChangeCamera2()
    {
        cameraList[0].gameObject.SetActive(false);
        cameraList[1].gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        changeCamera1.action.Disable();
        changeCamera2.action.Disable();
    }
}
