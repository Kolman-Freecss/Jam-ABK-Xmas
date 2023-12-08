#region

using Gameplay.Config;
using UnityEngine;
using UnityEngine.AI;

#endregion

public abstract class EnemyState : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform player;
    public Transform target;

    protected float distanceFromTarget;

    private void Update()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.m_player.transform;
        if (target == null)
        {
            target = player;
        }
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
