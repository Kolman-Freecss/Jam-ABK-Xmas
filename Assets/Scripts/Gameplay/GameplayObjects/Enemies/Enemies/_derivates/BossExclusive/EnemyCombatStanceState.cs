using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatStanceState : EnemyState
{
    EnemyAttackState attackState;
    EnemyPursueTargetState enemyPursueTargetState;
    EnemyManager enemyManager;
    EnemyStateManager enemyStateManager;
    EnemyCatchRange enemyCatchRange;

    protected override void Start()
    {
        base.Start();
        attackState = GetComponent<EnemyAttackState>();
        enemyPursueTargetState = GetComponent<EnemyPursueTargetState>();
        enemyStateManager = GetComponent<EnemyStateManager>();
        enemyManager = GetComponent<EnemyManager>();
        enemyCatchRange = GetComponent<EnemyCatchRange>();
    }

    public override EnemyState RunCurrentState()
    {
        distanceFromTarget = Vector3.Distance(transform.position, target.position);

        if (enemyManager.currentRecoveryTime <= 0 && enemyCatchRange.IsInCatchRange())
            return attackState;
        else if (!enemyCatchRange.IsInCatchRange())
            return enemyPursueTargetState;

        return this;
    }
}
