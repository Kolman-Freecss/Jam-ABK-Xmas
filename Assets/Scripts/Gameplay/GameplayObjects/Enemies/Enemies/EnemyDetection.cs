using System.Collections;
using System.Collections.Generic;
using Gameplay.Config;
using Gameplay.GameplayObjects.Character;
using Gameplay.GameplayObjects.Character.Player;
using Gameplay.GameplayObjects.Character.Stealth._impl;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{
    #region Variables

    [Header("References")]
    CharacterStealthBehaviour characterStealthBehaviour;
    EnemyStateManager enemyStateManager;
    RoundManager roundManager;

    [Header("Detection")]
    [SerializeField] UnityEvent onStartFollowing;

    float distanceToTarget;

    public float viewRadius;
    [Tooltip("Must be higher value than viewRadius")]
    public float leaveRadius;
    [SerializeField] float viewAngle;

    [Range(1, 4)]
    [SerializeField] int maxTimesDetected;
    [HideInInspector] public int timesDetected;

    [SerializeField] LayerMask isPlayer, isObstacle;
    Transform player;

    
    [Header("Timer")]
    private bool hasBeenSeen = false;
    private float chaseTimer;
    [SerializeField] float chaseTimerMax = 3f;

    float visionTimer;
    [SerializeField] float maxVisionTimer = 2f;
    

    [Header("Multipliers")]
    [SerializeField] float stealthMultiplier = 1.25f;
    [SerializeField] float hideMultiplier = 1.5f;
    #endregion

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterStealthBehaviour = player.GetComponent<PlayerStealthBehaviour>();
        roundManager = RoundManager.Instance;
    }

    private void OnEnable() 
    {
        characterStealthBehaviour.onStealthStatusChanged.AddListener(PlayerStateChange);
    }

    private void Update() 
    {
        Timer();
        if (!hasBeenSeen)
        {
            if (chaseTimer != 0f || visionTimer != 0f)
            {
                //if not seen but has been, as the timers are not zero
                chaseTimer = 0f;
                visionTimer = 0f;
                onStartFollowing?.Invoke();
                Detection();
            }
        }
    }

    //check whether player is being detected, called in EnemyIdleState
    public void OnPlayerDetection()
    {
        Vector3 playerTarget = (player.position - transform.position).normalized;

        //if player is inside the fov
        if(Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            Debug.Log("inside vision angle");
            distanceToTarget = Vector3.Distance(transform.position, player.position);

            //if player is near enough
            if (distanceToTarget <= viewRadius || hasBeenSeen)
            {
                Debug.Log("inside vision range");
                //if there isnt an obstacle between enemy vision and player, enemy is seeing the player
                if (Physics2D.Raycast(transform.position, playerTarget, distanceToTarget, isObstacle) == false)
                {
                    Debug.Log("seen");
                    hasBeenSeen = true;

                    if (visionTimer >= maxVisionTimer)
                        onStartFollowing?.Invoke();
                }
            }
        }
    }

    void PlayerStateChange(StealthStatus newState)
    {
        switch (newState)
        {
            default:
            case NormalState:
            chaseTimerMax = 3f;
            maxVisionTimer = 2f;

            break;

            case StealthState:
            chaseTimerMax *= stealthMultiplier;
            maxVisionTimer *= -stealthMultiplier;

            break;

            case HideState:
            chaseTimerMax *= hideMultiplier;
            maxVisionTimer *= -hideMultiplier;

            break;
        }
    }

    void Timer()
    {
        //some time after leaving its radius before leaving the chase
        if (distanceToTarget > leaveRadius && hasBeenSeen)
        {
            if (chaseTimer < chaseTimerMax)
                chaseTimer += Time.deltaTime;
            else
                hasBeenSeen = false;
        }

        else if (distanceToTarget < viewRadius && hasBeenSeen)
        {
            //timer so they dont get the agro instantly, they gotta keep watching you for some time
            if (visionTimer < maxVisionTimer)
                    visionTimer += Time.deltaTime;

            //reseting the timer when entering back again
            else if (visionTimer >= maxVisionTimer)
            {
                hasBeenSeen = true;
                if (chaseTimer != 0f)
                    chaseTimer = 0f;
            }
        }
    }

    public void Detection()
    {
        if (!hasBeenSeen)
        {
            for (int i = 0; i < roundManager.enemiesInScene.Count; i++)
            {
                EnemyDetection enemyDetection = roundManager.enemiesInScene[i].
                gameObject.GetComponent<EnemyDetection>();

                viewRadius *= stealthMultiplier;
                leaveRadius *= stealthMultiplier;

                enemyDetection.timesDetected++;
            }

            if (timesDetected == maxTimesDetected)
            {
                for (int i = 0; i < roundManager.enemiesInScene.Count; i++)
                {
                    EnemyDetection enemyDetection = roundManager.enemiesInScene[i].
                    gameObject.GetComponent<EnemyDetection>();

                    enemyDetection.viewRadius *= stealthMultiplier;
                    enemyDetection.leaveRadius *= stealthMultiplier;
                }
            }

            else if (timesDetected >= maxTimesDetected)
            {
                //call Boss
            }
        }
    }
}
