using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] float viewRadius;
    [SerializeField] float viewAngle;
    float distanceToTarget;

    [SerializeField] LayerMask isPlayer, isObstacle;
    Transform player;

    private bool hasBeenSeen;
    private float chaseTimer;
    [SerializeField] float chaseTimerMax = 3f;

    //check if player is being detected, called in EnemyIdleState
    public bool OnPlayerDetection()
    {
        Vector3 playerTarget = (player.position - transform.position).normalized;

        if(Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            Debug.Log("inside vision angle");
            distanceToTarget = Vector3.Distance(transform.position, player.position);
            if(distanceToTarget <= viewRadius || hasBeenSeen)
            {
                hasBeenSeen = true;
                Debug.Log("inside vision range");
                if (Physics2D.Raycast(transform.position, playerTarget, distanceToTarget, isObstacle) == false)
                {
                    Debug.Log("seen");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void u()
    {
        if (distanceToTarget > viewRadius && hasBeenSeen && chaseTimer < chaseTimerMax)
        {
            
        }
    }
}
