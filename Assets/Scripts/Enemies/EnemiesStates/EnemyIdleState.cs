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
        if (uwu())
            return enemyChaseState;

        else    
            return this;
    }

    private bool uwu()
    {
        return true;
    }
}
