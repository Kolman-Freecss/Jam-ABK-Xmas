using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPresents : Spawn
{
    #region Variables
    /* Public Variables */
    [SerializeField] private List<GameObject> presentPrefabs;
    [SerializeField]
    int secondsToSpawn = 1;

    /* Dictionary Variables */
    Dictionary<string, float> _points;
    GameObject[] presents;

    #endregion
    // Start is called before the first frame update
    void Start() {
        if (spawnPoints.Count != 4) return;

        _points = FindSpawnPoints();
        CanSpawn = true;
        Debug.Log("NumberSpawn: " + NumberSpawn);
        CanSpawn = true;
        if (NumberSpawn == 0 && CanSpawn)
        {
            StartCoroutine(SpawnWithDelay(secondsToSpawn, Random.Range(3, 6)));
        }
     }

    // Update is called once per frame
    void Update()
    {
        
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
        StartCoroutine(RemoveComponents());
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
        presents = new GameObject[amount];
        float height = 1.57f;
        for (var i = 0; i < amount; i++)
        {
            presents[i] = NewInstance(_points, presentPrefabs, height);
            NumberSpawn += 1;
        }
    }

    /// <summary>
    /// The function removes the Rigidbody component from a given GameObject after a delay of 1 second.
    /// </summary>
    /// <param name="GameObject">The parameter "present" is a reference to a GameObject.</param>
    IEnumerator RemoveComponents()
    {
        Debug.Log(presents.Length);
        foreach (GameObject present in presents)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("Removing components");
            present.GetComponent<Rigidbody>().isKinematic = true;
            present.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
