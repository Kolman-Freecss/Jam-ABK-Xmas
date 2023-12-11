#region

using UnityEngine;

#endregion

public class EnemyIdleState : EnemyState
{
    EnemyDetection enemyDetection;
    EnemyChaseState enemyChaseState;
    EnemyStateManager enemyStateManager;
    EnemyPursueTargetState enemyPursueTargetState;
    PatrolAI patrolAI;

    bool playerDetected;

    [Header("Debug")]
    [SerializeField]
    bool debugChangeState;

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
        patrolAI = GetComponent<PatrolAI>();
        enemyChaseState = GetComponent<EnemyChaseState>();
        enemyDetection = GetComponentInChildren<EnemyDetection>();
        enemyStateManager = GetComponent<EnemyStateManager>();
        enemyPursueTargetState = GetComponent<EnemyPursueTargetState>();
    }

    //if player seen, change to chase, otherwise just returns idle
    public override EnemyState RunCurrentState()
    {
        if (playerDetected && target != null)
        {
            if (enemyStateManager.myTag == "Boss")
                return enemyPursueTargetState;
            else
                return enemyChaseState;
        }
        else
        {
            enemyDetection.OnPlayerDetection();
            patrolAI.PatrolLogic();
        }
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
