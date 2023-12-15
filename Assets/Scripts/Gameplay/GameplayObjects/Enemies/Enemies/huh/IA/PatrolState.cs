using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AIState
{
    [SerializeField] Transform patrolPointsParent;
    [SerializeField] float reachingDistance = 1f;

    int currentPoint;

    public override void Enter()
    {
        
    }


    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        Vector3 destination = patrolPointsParent.GetChild(currentPoint).position;
        agent.SetDestination(destination);
        //mas eficiente que el distance por no hacer divisiones
        if ((transform.position - destination).sqrMagnitude < (reachingDistance * reachingDistance))
        {
            currentPoint++;
            if (currentPoint >= patrolPointsParent.childCount)
                currentPoint = 0;
        }
    }

    public override void Exit()
    {
        
    }
}
