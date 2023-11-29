using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyState currentState;
    
    private void Update() 
    {
        RunStateMachine();
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
