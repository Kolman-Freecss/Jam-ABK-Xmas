using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionMaker : MonoBehaviour, IVisible
{
    [SerializeField] string allegiance = "Enemy";
    [SerializeField] float shootRange = 1f;
    private bool hasAlreadySeenPlayer;
    public float valiantRange = 5f;

    EntitySight entitySight;
    
    [SerializeField] AIState startState;

    #region States
    IdleState idleState;
    MeleeAttackState meleeAttackState;
    PatrolState patrolState;
    SeekingState seekingState;
    private EnemyCatchState enemyCatchState;
    LookingInLastPerceivedPosState perceivedPosState;
    #endregion

    AIState[] states;
    AIState currentState;

    public Transform target { get; private set; }

    Vector3 lastPerceivedPosition;
    bool hasLastPerceivedPosition;

    private void Awake() 
    {
        states = GetComponents<AIState>();

        idleState = GetComponent<IdleState>();
        meleeAttackState = GetComponent<MeleeAttackState>();
        patrolState = GetComponent<PatrolState>();
        seekingState = GetComponent<SeekingState>();
        perceivedPosState = GetComponent<LookingInLastPerceivedPosState>();
        enemyCatchState = GetComponent<EnemyCatchState>();

        entitySight = GetComponentInChildren<EntitySight>();

        perceivedPosState.onPerceivePosition.AddListener(OnLastPerceivedPositionReached);
    }

    private void Start() 
    {
        foreach (AIState s in states)
        {
            s.aIDecisionMaker = this;
        }
        SetState(startState);
    }

    private void Update() 
    {
        TargetFinding();
    }

    void TargetFinding()
    {
        Transform visibleTarget = entitySight.visiblesInSight.Find(
            (x) => x.GetAllegiance() != GetAllegiance())?.GetTransform();
        
        target = visibleTarget;

        bool canSeeTarget = entitySight.visiblesInSight.Find(
            (x) => x.GetTransform() == target) != null;
        
        gameObject.SetActive(false);
        
        if (target)
        {
            lastPerceivedPosition = target.position;
            hasLastPerceivedPosition = true;
            
            if (canSeeTarget)
            {
                if (Vector3.Distance(target.position, transform.position) < shootRange)
                {
                    SetState(meleeAttackState);
                }
                else
                {
                    SetState(seekingState);
                }
                    
            }
        }
        else if (hasLastPerceivedPosition)
        {
            perceivedPosState.lastPerceivedPos = lastPerceivedPosition;
            SetState(perceivedPosState);
        }
        else if (!hasAlreadySeenPlayer)
            SetState(idleState);
        else
            SetState(patrolState);
    }

    void SetState(AIState newState)
    {
        if (currentState != newState)
        {
            currentState?.Exit();
            foreach (AIState s in states)
            {
                if (s == newState)
                {
                    s.enabled = true;
                    s.Enter();
                }
                else
                    s.enabled = false;
            }
        }
        currentState = newState;
    }

    void OnLastPerceivedPositionReached()
    {
        hasLastPerceivedPosition = false;
    }

    #region IVisible
    public Transform GetTransform() {return transform;}
    public string GetAllegiance() {return allegiance;}

    #endregion
}
