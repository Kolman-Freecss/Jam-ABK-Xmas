#region

using UnityEngine;

#endregion

public class EnemyCatchRange : MonoBehaviour
{
    GameObject player;

    [SerializeField]
    float catchRange;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //comunicate wheither player is in catch range to EnemyChaseState
    public bool IsInCatchRange()
    {
        //TODO: Temporal fix
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 enemyPos = new Vector3(transform.position.x, 0, transform.position.z);
        return Vector3.Distance(enemyPos, playerPos) <= catchRange;
    }
}
