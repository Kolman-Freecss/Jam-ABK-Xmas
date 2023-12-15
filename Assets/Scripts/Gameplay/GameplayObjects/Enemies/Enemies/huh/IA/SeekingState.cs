using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingState : AIState
{
    private void Update() 
    {
        agent.SetDestination(aIDecisionMaker.target.position);
    }
}
