using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookingInLastPerceivedPosState : AIState
{
    [HideInInspector] public UnityEvent onPerceivePosition;
    public Vector3 lastPerceivedPos;
    [SerializeField] float reachingDistance;

    private void Update() 
    {
        agent.SetDestination(lastPerceivedPos);
        if (Vector3.Distance(lastPerceivedPos, transform.position) < reachingDistance)
        {
            onPerceivePosition?.Invoke();
        }
    }
}
