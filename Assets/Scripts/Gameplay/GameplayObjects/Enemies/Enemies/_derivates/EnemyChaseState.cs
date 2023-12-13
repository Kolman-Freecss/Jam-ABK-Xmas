#region

using UnityEngine;
using UnityEngine.AI;

#endregion

public class EnemyChaseState : EnemyState
{
    EnemyDetection enemyDetection;
    EnemyCatchState enemyCatchState;
    EnemyLookLastTargetPosState enemyLookLastPos;
    EnemyCatchRange enemyCatchRange;

    Vector3 lastPerceivedPos;
    bool hasLastPerceivedPosition;
    bool targetIsPlayer;

    [Header("Debug")]
    [SerializeField]
    bool debugChangeState;

    private void OnValidate()
    {
        if (debugChangeState)
        {
            debugChangeState = false;
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        enemyDetection = GetComponentInChildren<EnemyDetection>();
        enemyCatchState = GetComponent<EnemyCatchState>();
        enemyCatchRange = GetComponent<EnemyCatchRange>();
        enemyLookLastPos = GetComponent<EnemyLookLastTargetPosState>();

        if (!enemyDetection)
        {
            Debug.LogError("EnemyDetection not found in children");
        }
        else
        {
            enemyDetection.onStartFollowing.AddListener(TargetAsignation);
        }
        if (!enemyLookLastPos)
        {
            Debug.LogError("EnemyLookLastTargetPosState not found");
        }
        else
        {
            enemyLookLastPos.onPerceivePosition.AddListener(OnLastPerceivedPositionReached);
        }
    }

    //if player in catch range, change to attack
    public override EnemyState RunCurrentState()
    {
        if (targetIsPlayer)
            target = player;
        else
            target = null;

        if (!target)
        {
            lastPerceivedPos = target.position;
            hasLastPerceivedPosition = true;
        }

        if (enemyCatchRange.IsInCatchRange() || debugChangeState)
            return enemyCatchState;
        else if (!enemyCatchRange.IsInCatchRange() && !target)
        {
            if (hasLastPerceivedPosition)
            {
                enemyLookLastPos.lastPerceivedPos = this.lastPerceivedPos;
                return enemyLookLastPos;
            }
            else
            {
                return this;
            }
        }
        else
        {
            ChaseLogic();
            return this;
        }
    }

    private void TargetAsignation()
    {
        if (!targetIsPlayer)
            targetIsPlayer = true;
        else
            targetIsPlayer = false;
    }

    private void ChaseLogic()
    {
        //agent.SetDestination(target.position);
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.position, path);
        agent.SetPath(path);
    }

    void OnLastPerceivedPositionReached()
    {
        hasLastPerceivedPosition = false;
    }
}
