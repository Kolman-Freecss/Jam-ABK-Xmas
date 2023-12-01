#region

using UnityEngine;

#endregion

public class EnemyCatchRange : MonoBehaviour
{
    GameObject player;

    [SerializeField] float catchRange;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //comunicate neither player is in catch range to EnemyChaseState
    public bool IsInCatchRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= catchRange;
    }
}