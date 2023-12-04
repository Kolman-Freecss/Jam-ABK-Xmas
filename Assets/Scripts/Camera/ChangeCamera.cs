using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private InputActionReference change;


    public GameObject[] listaCamaras;
    private void OnEnable()
    {
        change.action.Enable();
    }

    private void Start()
    {
        listaCamaras[0].gameObject.SetActive(true);
        listaCamaras[1].gameObject.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            listaCamaras[0].gameObject.SetActive(false);
            listaCamaras[1].gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            listaCamaras[0].gameObject.SetActive(true);
            listaCamaras[1].gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        change.action.Disable();
    }






}
