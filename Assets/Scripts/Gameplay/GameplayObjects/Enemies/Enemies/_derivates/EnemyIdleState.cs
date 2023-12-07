using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    EnemyDetection enemyDetection;
    EnemyChaseState enemyChaseState;
    EnemyStateManager enemyStateManager;
    EnemyPursueTargetState enemyPursueTargetState;

    bool playerDetected;

    [Header("Debug")]
    [SerializeField] bool debugChangeState;

    private void OnValidate() 
    {
        if (debugChangeState)
        {
            debugChangeState = false;
            playerDetected = true;
        }
    }

    private void Start() 
    {
        enemyChaseState = GetComponent<EnemyChaseState>();
        enemyDetection = GetComponent<EnemyDetection>();
        enemyStateManager = GetComponent<EnemyStateManager>();
        enemyPursueTargetState = GetComponent<EnemyPursueTargetState>();
    }

    //if player seen, change to chase, otherwise just returns idle
    public override EnemyState RunCurrentState()
    {
        if (playerDetected && target != null)
        {
            if (enemyStateManager.tag == "Boss")
                return enemyPursueTargetState;
            else
                return enemyChaseState;
        }

        else
            enemyDetection.OnPlayerDetection();
            return this;
    }

    public void OnPlayerDetected()
    {
        if (!playerDetected)
            playerDetected = true;
        else
            playerDetected = false;
    }
}
