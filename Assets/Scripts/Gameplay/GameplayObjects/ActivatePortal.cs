using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortal : MonoBehaviour
{
    void Start()
    {
        //Deactivates the portal by default.
        DeactivatePortal();
    }

    void DeactivatePortal()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        // Activates the portal when it's called from presentsNeeded script.
        gameObject.SetActive(true);
    }
}
