using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursueTargetState : EnemyState
{
    [SerializeField] float seekFactor = 1f;
    [SerializeField] float valiantRange = 1.5f;
    [SerializeField] float rotationSpeed = 50f;

    EnemyStateManager enemyStateManager;
    EnemyManager enemyManager;
    EnemyCatchRange enemyCatchRange;
    EnemyCombatStanceState enemyCombatStanceState;

    private void Start() 
    {
        enemyStateManager = GetComponent<EnemyStateManager>();
        enemyManager = GetComponent<EnemyManager>();
        enemyCatchRange = GetComponent<EnemyCatchRange>();
        enemyCombatStanceState = GetComponent<EnemyCombatStanceState>();
        target = player;
    }

    public override EnemyState RunCurrentState()
    {
        if (enemyManager.isPerformingAction)
            return this;

        Vector3 targetDirection = target.position - transform.position;
        distanceFromTarget = Vector3.Distance(transform.position, target.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if (distanceFromTarget > agent.stoppingDistance)
        {
            //anim
        }

        RotateTowardsCharacter();
        //UpdateGetCloserAndMoveAround();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (enemyCatchRange.IsInCatchRange())
            return enemyCombatStanceState;
        else
            return this;
        
    }

    private void RotateTowardsCharacter()
    {
        if (enemyManager.isPerformingAction)
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            dir.Normalize();

            if (dir == Vector3.zero)
            {
                dir = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
        }
        else
        {
            Vector3 relativeDir = transform.InverseTransformDirection(agent.desiredVelocity);
            
            agent.enabled = true;
            agent.SetDestination(target.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, agent.transform.rotation, rotationSpeed / Time.deltaTime);
        }
    }

    void UpdateGetCloserAndMoveAround()
    {
        Vector3 desiredPosition = transform.position + transform.right;
        if (Vector3.Distance(transform.position, target.position) > valiantRange)
        { 
            Vector3 direction = target.position - transform.position;
            desiredPosition += direction.normalized * seekFactor;
        }
        agent.SetDestination(target.position);
    }
}
