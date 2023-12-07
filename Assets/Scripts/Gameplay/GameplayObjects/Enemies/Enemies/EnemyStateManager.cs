using System.Collections;
using System.Collections.Generic;
using Gameplay.Config;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateManager : MonoBehaviour
{
    EnemyState currentState;

    EnemyIdleState enemyIdleState;
    
    public string myTag = "Enemy";

    private void Awake() 
    {
        RoundManager.Instance.enemiesInScene.Add(this);
    }

    private void Start() 
    {
        enemyIdleState = GetComponent<EnemyIdleState>();

        currentState = enemyIdleState;
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
