using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] float viewRadius;
    [Tooltip("Must be higher value than viewRadius")]
    [SerializeField] float leaveRadius;
    [SerializeField] float viewAngle;
    float distanceToTarget;

    [SerializeField] LayerMask isPlayer, isObstacle;
    Transform player;
    

    [Header("Timer")]
    private bool hasBeenSeen = false;
    private float chaseTimer;
    [SerializeField] float chaseTimerMax = 3f;

    private void Update() 
    {
        Timer();
    }

    //check whether player is being detected, called in EnemyIdleState
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

    void Timer()
    {
        if (distanceToTarget > leaveRadius && hasBeenSeen)
        {
            if (chaseTimer < chaseTimerMax)
            {
                chaseTimer += Time.deltaTime;
            }
            else
            {
                hasBeenSeen = false;
            }
        }
    }
}
