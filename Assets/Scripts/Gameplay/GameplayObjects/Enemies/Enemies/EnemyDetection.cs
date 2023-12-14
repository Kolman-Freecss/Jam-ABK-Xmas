#region

using Gameplay.Config;
using Gameplay.GameplayObjects.Character;
using Gameplay.GameplayObjects.Character.Player;
using Gameplay.GameplayObjects.Character.Stealth._impl;
using UnityEngine;
using UnityEngine.Events;

#endregion

public class EnemyDetection : MonoBehaviour
{
    #region Variables

    [Header("References")]
    CharacterStealthBehaviour characterStealthBehaviour;
    EnemyIdleState enemyIdleState;

    [Header("Detection")]
    public UnityEvent onStartFollowing;

    float distanceToTarget;

    public float viewRadius;

    [Tooltip("Must be higher value than viewRadius")]
    public float leaveRadius;

    [SerializeField]
    float viewAngle;

    [Range(1, 4)]
    [SerializeField]
    int maxTimesDetected;

    [HideInInspector]
    public int timesDetected;

    [SerializeField]
    LayerMask isPlayer,
        isObstacle;
    Transform player;

    [Header("Timer")]
    private bool hasBeenSeen = false;
    private float chaseTimer;

    [SerializeField]
    float chaseTimerMax = 3f;

    float visionTimer;

    [SerializeField]
    float maxVisionTimer = 2f;

    [Header("Multipliers")]
    [SerializeField]
    float stealthMultiplier = 1.25f;

    [SerializeField]
    float hideMultiplier = 1.5f;
    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterStealthBehaviour = player.GetComponent<PlayerStealthBehaviour>();
        enemyIdleState = GetComponentInParent<EnemyIdleState>();
    }

    private void OnEnable()
    {
        characterStealthBehaviour.onStealthStatusChanged.AddListener(PlayerStateChange);
    }

    private void Update()
    {
        OnPlayerDetection();
        if (!hasBeenSeen)
        {
            if (chaseTimer != 0f || visionTimer != 0f)
            {
                //if not seen but has been, as the timers are not zero
                chaseTimer = 0f;
                visionTimer = 0f;
                onStartFollowing?.Invoke();
            }
        }
    }

    //check whether player is being detected, called in EnemyIdleState
    public void OnPlayerDetection()
    {
        Vector3 playerTarget = (player.position - transform.position).normalized;

        //if player is inside the fov
        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            Debug.Log("inside vision angle");
            distanceToTarget = Vector3.Distance(transform.position, player.position);

            //if player is near enough
            if (distanceToTarget <= viewRadius)
            {
                Debug.Log("inside vision range");
                //if there isnt an obstacle between enemy vision and player, enemy is seeing the player
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, isObstacle) == false)
                {
                    Debug.Log("seen");
                    hasBeenSeen = true;
                    
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
                maxVisionTimer = 2f;

                break;

            case StealthState:
                maxVisionTimer *= -stealthMultiplier;

                break;

            case HideState:
                maxVisionTimer *= -hideMultiplier;

                break;
        }
    }

    public void Detection()
    {
        if (!hasBeenSeen)
        {
            for (int i = 0; i < RoundManager.Instance.enemiesInScene.Count; i++)
            {
                EnemyDetection enemyDetection = RoundManager
                    .Instance
                    .enemiesInScene[i]
                    .gameObject
                    .GetComponent<EnemyDetection>();

                viewRadius *= stealthMultiplier;

                enemyDetection.timesDetected++;
            }

            if (timesDetected == maxTimesDetected)
            {
                for (int i = 0; i < RoundManager.Instance.enemiesInScene.Count; i++)
                {
                    EnemyDetection enemyDetection = RoundManager
                        .Instance
                        .enemiesInScene[i]
                        .gameObject
                        .GetComponent<EnemyDetection>();

                    enemyDetection.viewRadius *= stealthMultiplier;
                }
            }
            else if (timesDetected >= maxTimesDetected)
            {
                RoundManager.Instance.BossCall();
            }
        }
    }
}
