using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossRun : StateMachineBehaviour
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange = 3f;

    Transform player;
    NavMeshAgent agent;
    Boss boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<Boss>();
        agent = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        if (Vector3.Distance(animator.transform.position, player.position) <= attackRange)
        {
            agent.isStopped = true;
            animator.SetTrigger("Attack");
        }
        else
        {
            agent.isStopped = false;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
