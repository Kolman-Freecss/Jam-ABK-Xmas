using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPresents : Spawn
{
    #region Variables
    /* Public Variables */
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField] int secondsToSpawn = 1;

    /* Dictionary Variables */
    private Dictionary<string, float> _points;

    #endregion
    // Start is called before the first frame update
    void Start() { }
    
    // Update is called once per frame
    void Update() { 

        //TODO: Check if the player solved the puzzle
        if (NumberSpawn == 0 && CanSpawn)
        {
            StartCoroutine(SpawnWithDelay(secondsToSpawn, Random.Range(3, 6)));
        }
    }

    #region Overrides
    /// <summary>
    /// This function spawns a specified number of instances with a delay between each spawn.
    /// </summary>
    /// <param name="time">The time parameter is the delay in seconds before the spawning
    /// occurs.</param>
    /// <param name="amount">The number of instances to create.</param>
    protected override IEnumerator SpawnWithDelay(int time, int amount = 1)
    {
        CanSpawn = false;
        yield return new WaitForSeconds(time);
        createInstance(amount);
        CanSpawn = true;
    }
    #endregion

    /// <summary>
    /// The function creates a specified number of instances of an enemy prefab at a given height.
    /// </summary>
    /// <param name="amount">The "amount" parameter is an integer that represents the number of
    /// instances to create.</param>
    public void createInstance(int amount)
    {

        //TODO: Check the height depending on the layer
        float height = 1.57f;
        for (var i = 0; i < amount; i++)
        {
            NewInstance(_points, new List<GameObject> { enemyPrefab }, height);
        }
    }
}
