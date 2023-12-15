using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    private void Update()
    {
        agent.SetDestination(transform.position);
    }
}
