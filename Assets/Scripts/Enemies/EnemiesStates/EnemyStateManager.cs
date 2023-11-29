using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyState currentState;

    EnemyIdleState enemyIdleState;
    EnemyChaseState enemyChaseState;
    EnemyAttackState enemyAttackState;

    private void Start() 
    {
        enemyIdleState = GetComponent<EnemyIdleState>();
        enemyChaseState = GetComponent<EnemyChaseState>();
        enemyAttackState = GetComponent<EnemyAttackState>();
    }
    
    private void Update() 
    {
        RunStateMachine();
        Debug.Log(currentState);
    }

    private void RunStateMachine()
    {
        EnemyState nextState = currentState?.RunCurrentState();

        if (nextState != null)
            SwitchToNextState(nextState);
    }

    private void SwitchToNextState(EnemyState nextState)
    {
        currentState = nextState;
    }
}
