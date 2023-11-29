using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    EnemyDetection enemyDetection;
    EnemyChaseState enemyChaseState;

    private void Start() 
    {
        enemyChaseState = GetComponent<EnemyChaseState>();
        enemyDetection = GetComponent<EnemyDetection>();
    }

    //if player seen, change to chase, otherwise just returns idle
    public override EnemyState RunCurrentState()
    {
        if (enemyDetection.OnPlayerDetection())
            return enemyChaseState;

        else    
            return this;
    }

    
}
