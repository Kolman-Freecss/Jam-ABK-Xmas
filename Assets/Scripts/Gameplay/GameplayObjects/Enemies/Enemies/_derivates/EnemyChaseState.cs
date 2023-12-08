using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    EnemyDetection enemyDetection;
    EnemyAttackState enemyAttackState;
    EnemyLookLastTargetPosState enemyLookLastPos;
    EnemyCatchRange enemyCatchRange;

    Vector3 lastPerceivedPos;
    bool hasLastPerceivedPosition;
    bool targetIsPlayer;

    [Header("Debug")]
    [SerializeField] bool debugChangeState;

    private void OnValidate() 
    {
        if (debugChangeState)
        {
            debugChangeState = false;
        }
    }

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();

        enemyDetection = GetComponentInChildren<EnemyDetection>();
        enemyAttackState = GetComponent<EnemyAttackState>();
        enemyCatchRange = GetComponent<EnemyCatchRange>();
        enemyLookLastPos = GetComponent<EnemyLookLastTargetPosState>();

        enemyDetection.onStartFollowing.AddListener(TargetAsignation);
        enemyLookLastPos.onPerceivePosition.AddListener(OnLastPerceivedPositionReached);
    }

    //if player in catch range, change to attack
    public override EnemyState RunCurrentState()
    {
        if (targetIsPlayer)
            target = player;
        else
            target = null;

        lastPerceivedPos = target.position;
        hasLastPerceivedPosition = true;

        if (enemyCatchRange.IsInCatchRange() || debugChangeState)
            return enemyAttackState;

        else if (!enemyCatchRange.IsInCatchRange() && !target)
        {
            if (hasLastPerceivedPosition)
            {
                enemyLookLastPos.lastPerceivedPos = this.lastPerceivedPos;
                return enemyLookLastPos;
                
            }
            else
            {
                return this;
            }
        }

        else
        {
            ChaseLogic();
            return this;
        }
    }
    
    private void TargetAsignation()
    {
        if (!targetIsPlayer)
            targetIsPlayer = true;
        else
            targetIsPlayer = false;
    }

    private void ChaseLogic()
    {
        //agent.SetDestination(target.position);
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.position, path);
        agent.SetPath(path);
    }

    void OnLastPerceivedPositionReached()
    {
        hasLastPerceivedPosition = false;
    }
}
