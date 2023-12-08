using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivatePortal : MonoBehaviour
{
    public float useDistance = 2f; // Distance at which the portal can be used

    bool portalUsable = false;

    void Start()
    {
        //Deactivates the portal by default.
        DeactivatePortal();
        portalUsable = false;
    }

    private void Update()
    {
        if (portalUsable && Input.GetKeyDown(KeyCode.E))
        {
            if (PortalPlayerDistance() <= useDistance)
            {
                UsePortal();
            }
        }
    }

    float PortalPlayerDistance()
    {
        // Calculates the distance betwwen the portal and the player.
        if (portalUsable)
        {
            return Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        }

        return Mathf.Infinity;
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifies that the player is on the use zone of the portal.
        if (other.CompareTag("Player"))
        {
            portalUsable = true;
        }
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

    public void UsePortal()
    {
        SceneManager.LoadScene("Hell");
    }
}
