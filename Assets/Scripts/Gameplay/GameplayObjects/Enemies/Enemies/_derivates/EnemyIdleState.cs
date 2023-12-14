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

    [Header("Stun")]
    //start not stunned
    private bool stunnedTimerOn = false;
    private float timer;

    [SerializeField]
    float maxStunnedTime = 3f;

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

    protected override void Start()
    {
        base.Start();
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
            //if its stunned, neither patrol nor detection is called
            if (stunnedTimerOn)
            {
                timer += Time.deltaTime;
                if (timer >= maxStunnedTime)
                {
                    timer = 0;
                    stunnedTimerOn = false;
                }
            }
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

    public void SwitchTimer()
    {
        if (stunnedTimerOn)
            stunnedTimerOn = false;
        else
            stunnedTimerOn = true;
    }
}
