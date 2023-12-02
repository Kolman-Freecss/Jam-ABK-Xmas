using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] float speed = 5f;
    [SerializeField] float waitTime = 1f;
    bool isWaiting;
    [SerializeField] Transform[] waypoints;

    int currentWaypoint = 0;

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void PatrolLogic()
    {
        if(transform.position != waypoints[currentWaypoint].position)
        {
            agent.SetDestination(waypoints[currentWaypoint].position * Time.deltaTime);

        }
        else if(!isWaiting)
        {
            StartCoroutine(Wait());
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
