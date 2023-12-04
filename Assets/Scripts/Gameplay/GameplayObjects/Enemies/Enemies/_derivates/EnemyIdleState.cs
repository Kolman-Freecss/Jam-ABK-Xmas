#region

using UnityEngine;

#endregion

public class EnemyIdleState : EnemyState
{
    EnemyDetection enemyDetection;
    EnemyChaseState enemyChaseState;

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
        enemyChaseState = GetComponent<EnemyChaseState>();
        enemyDetection = GetComponentInChildren<EnemyDetection>();
    }

    //if player seen, change to chase, otherwise just returns idle
    public override EnemyState RunCurrentState()
    {
        if (playerDetected)
            return enemyChaseState;
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
