#region

using UnityEngine;

#endregion

public class EnemyCatchState : EnemyState
{
    EnemyCatchRange enemyCatchRange;
    EnemyChaseState enemyChaseState;
    EnemyIdleState enemyIdleState;

    private void Start()
    {
        enemyCatchRange = GetComponent<EnemyCatchRange>();
        enemyChaseState = GetComponent<EnemyChaseState>();
    }

    public override EnemyState RunCurrentState()
    {
        Debug.Log("Catch");
        if (enemyCatchRange.IsInCatchRange())
        {
            enemyStateManager.enemyObserver.Notify(this);
            return enemyIdleState;
        }
        else
            return enemyChaseState;
    }
}
