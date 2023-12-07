using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyLookLastTargetPosState : EnemyState
{
    public UnityEvent onPerceivePosition;
    public Vector3 lastPerceivedPos;
    [SerializeField] float reachingDistance;

    public override EnemyState RunCurrentState()
    {
        agent.SetDestination(lastPerceivedPos);
        if (Vector3.Distance(lastPerceivedPos, transform.position) < reachingDistance)
        {
            onPerceivePosition?.Invoke();
        }
        return this;
    }
}
