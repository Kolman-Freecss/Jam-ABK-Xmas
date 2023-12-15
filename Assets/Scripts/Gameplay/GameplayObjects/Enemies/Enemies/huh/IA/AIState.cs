using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIState : MonoBehaviour
{
    [HideInInspector] public AIDecisionMaker aIDecisionMaker;

    protected NavMeshAgent agent;
    protected Animator anim;
    public Transform target;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    public virtual void Enter(){}
    public virtual void Exit(){}
}
