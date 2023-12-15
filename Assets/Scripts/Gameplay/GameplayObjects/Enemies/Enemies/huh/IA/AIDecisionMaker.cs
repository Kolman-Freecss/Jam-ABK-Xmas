using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionMaker : MonoBehaviour, IVisible
{
    [SerializeField] string allegiance = "Enemy";
    [SerializeField] float shootRange = 15f;
    private bool hasAlreadySeenPlayer;
    public float valiantRange = 5f;

    EntitySight entitySight;
    EntityAudition entityAudition;
    
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
        entityAudition = GetComponentInChildren<EntityAudition>();

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

        Transform audibleTarget =
        entityAudition.heardAudibles.Find(
            (x) => x.GetAllegiance() != GetAllegiance())?.audible.transform;
        
        target = null;
        
        if (!visibleTarget)
            target = audibleTarget;
        else if (audibleTarget)
        {
            target = Vector3.Distance(visibleTarget.position, transform.position) <
                Vector3.Distance(audibleTarget.position, transform.position) ? 
                visibleTarget : audibleTarget;
        }
        else
            target = visibleTarget;

        bool canSeeTarget = entitySight.visiblesInSight.Find(
            (x) => x.GetTransform() == target) != null;
        
        bool canHearTarget = entityAudition.heardAudibles.Find(
            (x) => x.audible.transform == target) != null;
        
        gameObject.SetActive(false);
        
        if (target)
        {
            //si es ambusher, se convierte en valiente al verte
            lastPerceivedPosition = target.position;
            //tiene lastPerceivedPosition en el caso de que no sea guardian
            hasLastPerceivedPosition = true;
            
            if (canSeeTarget)
            {
                if (Vector3.Distance(target.position, transform.position) < shootRange)
                {
                    SetState(enemyCatchState);
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
