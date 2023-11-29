using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    EnemyAttackState enemyAttackState;
    EnemyCatchRange enemyCatchRange;

    private void Start() 
    {
        enemyAttackState = GetComponent<EnemyAttackState>();
        enemyCatchRange = GetComponent<EnemyCatchRange>();
    }

    //if player in catch range, change to attack
    public override EnemyState RunCurrentState()
    {
        if (enemyCatchRange.IsInCatchRange())
            return enemyAttackState;
        else
            return this;
    }
}
