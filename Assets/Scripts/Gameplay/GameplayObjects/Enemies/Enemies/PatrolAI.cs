#region

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

#endregion

public class PatrolAI : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float waitTime = 1f;
    bool isWaiting;

    [SerializeField]
    Transform[] waypoints;

    int currentWaypoint = 0;

    [SerializeField] Animator animator;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public void PatrolLogic()
    {
        Vector3 AIPosition = new Vector3(transform.position.x, 0, (float)Math.Truncate(transform.position.z));
        Vector3 waypointPosition = new Vector3(
            waypoints[currentWaypoint].position.x,
            0,
            (float)Math.Truncate(waypoints[currentWaypoint].position.z)
        );
        if (AIPosition != waypointPosition)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);

           animator.SetBool("Walk", true);  

        }
        else if (!isWaiting)
        {
            StartCoroutine(Wait());
            animator.SetBool("Walk", false);
        }
    }

    IEnumerator Wait()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
            currentWaypoint = 0;

        isWaiting = false;
    }
}
