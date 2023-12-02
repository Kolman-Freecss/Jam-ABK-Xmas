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

    [Header("Debug")]
    [SerializeField] bool debugChangeState;

    private void OnValidate() 
    {
        if (debugChangeState)
        {
            debugChangeState = false;
        }
    }

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyAttackState = GetComponent<EnemyAttackState>();
        enemyCatchRange = GetComponent<EnemyCatchRange>();
    }

    //if player in catch range, change to attack
    public override EnemyState RunCurrentState()
    {
        if (enemyCatchRange.IsInCatchRange() || debugChangeState)
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
