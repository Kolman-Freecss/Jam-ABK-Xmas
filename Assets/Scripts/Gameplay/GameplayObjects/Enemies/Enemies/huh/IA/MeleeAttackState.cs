using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttackState : AIState
{
    [Header("References")]
    [SerializeField] float attackDistance = 1f;

    public override void Enter()
    {

    }

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update() 
    {
        TargetFinding();
    }

    void TargetFinding()
    {
        Vector3 destination = aIDecisionMaker.target? aIDecisionMaker.target.position : transform.position;
        agent.SetDestination(destination);
        
        anim.SetBool("IsAttacking", target? 
        (Vector3.Distance(aIDecisionMaker.target.position, transform.position) 
        < attackDistance) : false);
    }

    public override void Exit()
    {
        
    }
}
