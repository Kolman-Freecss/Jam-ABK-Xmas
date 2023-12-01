using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    EnemyCatchRange enemyCatchRange;
    EnemyChaseState enemyChaseState;

    private void Start() 
    {
        enemyCatchRange = GetComponent<EnemyCatchRange>();
    }

    public override EnemyState RunCurrentState()
    {
        Debug.Log("attack");
        if (enemyCatchRange.IsInCatchRange())
            //attack
            return this;
        else
            return enemyChaseState;
    }
}
