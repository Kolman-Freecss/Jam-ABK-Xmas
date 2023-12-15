#region

using UnityEngine;

#endregion

public class EnemyCatchState : AIState
{
    EnemyCatchRange enemyCatchRange;
    EnemyChaseState enemyChaseState;
    EnemyIdleState enemyIdleState;
    private EnemyDetection enemyDetection;

    private void Start()
    {
        enemyCatchRange = GetComponent<EnemyCatchRange>();
        enemyChaseState = GetComponent<EnemyChaseState>();
        enemyDetection = GetComponentInChildren<EnemyDetection>();
    }

    private void Update()
    {
        Debug.Log("Catch");
        if (enemyCatchRange.IsInCatchRange())
        {
            //enemyStateManager.enemyObserver.Notify(this);
        }
    }
}
