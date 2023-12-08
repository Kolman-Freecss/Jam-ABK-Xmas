using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform player;
    public Transform target;

    protected float distanceFromTarget;

    private void Update() 
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null)
        {
            distanceFromTarget = Vector3.Distance(transform.position, target.position);
        }
        else
        {
            distanceFromTarget = 0;
        }
    }
    
    //used in its heritage
    public abstract EnemyState RunCurrentState();
}
