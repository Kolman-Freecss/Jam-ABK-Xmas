using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    NavMeshAgent agent;
    Transform player;

    EnemyAttackState enemyAttackState;
    EnemyCatchRange enemyCatchRange;

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();

        enemyAttackState = GetComponent<EnemyAttackState>();
        enemyCatchRange = GetComponent<EnemyCatchRange>();
    }

    //if player in catch range, change to attack
    public override EnemyState RunCurrentState()
    {
        if (enemyCatchRange.IsInCatchRange())
            return enemyAttackState;
        else
            ChaseLogic();
            return this;
    }

    private void ChaseLogic()
    {
        agent.SetDestination(player.position);
    }
}
