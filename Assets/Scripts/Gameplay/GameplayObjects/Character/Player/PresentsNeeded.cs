using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentsNeeded : MonoBehaviour
{
    public int stolenPresents;
    public int presentsNeeded = 4;

    ActivatePortal activatePortal;

    private void Start()
    {
        activatePortal = GetComponent<ActivatePortal>();
    }

    //Function to check if the presents that we've stolen is the amount needed
    void PresentsNeededReached()
    {
        if (stolenPresents >= presentsNeeded) 
        {
            //if that's the case then active the portal to leave the level.
            ActivatePortal();
        }
    }
    
    void ActivatePortal()
    {
        //Quick call to the function Activate() in the activatePortal script.
        activatePortal.Activate();
    }
}
