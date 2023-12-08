using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentGrab : MonoBehaviour
{
    private GameObject grabbedPresent;
    public int deletedCubeCount = 0;

    // Update is called once per frame
    void Update()
    {
        // Check for user input
        if (Input.GetKeyDown(KeyCode.E)) GrabReleasePresent();
    }

    void GrabReleasePresent()
    {
        // If an object is already grabbed, release it, else try to grab one
        if (grabbedPresent != null) ReleasePresent();
        else GrabPresent();
    }

    void GrabPresent()
    {
        // Raycast to detect objects in front of the character
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            // Check if the hit object has a Rigidbody (can be grabbed)
            Rigidbody targetRigidbody = hit.collider.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                // Grab the object by making it kinematic
                grabbedPresent = targetRigidbody.gameObject;
                grabbedPresent.GetComponent<Rigidbody>().isKinematic = true;
                grabbedPresent.transform.parent = transform;
            }
        }
    }

    void ReleasePresent()
    {
        // Release the grabbed object
        if (grabbedPresent != null)
        {
            // Check if the released object has the "PresentType" tag
            if (grabbedPresent.CompareTag("PresentType"))
            {
                // Check if the released object is colliding with a cylinder with the "ContainerPresent" tag
                Collider[] colliders = Physics.OverlapBox(grabbedPresent.transform.position, grabbedPresent.transform.localScale / 2f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("ContainerPresent"))
                    {
                        // Destroy the grabbed object if it collides with the container
                        Destroy(grabbedPresent);
                        grabbedPresent = null;

                        // Increment the deleted cube count
                        deletedCubeCount++;
                        return;
                    }
                }
            }

            // If the released object doesn't meet the conditions for destruction, release it normally
            grabbedPresent.GetComponent<Rigidbody>().isKinematic = false;
            grabbedPresent.transform.parent = null;
            grabbedPresent = null;
        }
    }

    // Getter function
    public int GetDeletedPresents()
    {
        return deletedCubeCount;
    }
}