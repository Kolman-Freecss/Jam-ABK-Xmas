using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Spawn : MonoBehaviour
{
    #region Variables
    [SerializeField] protected List<Transform> spawnPoints;
    
    /* Bool Variables */
    protected bool CanSpawn;
    
    /* Int Variables */
    protected int NumberSpawn;
    #endregion
    
    #region Implemented Methods

    /// <summary>
    /// The function creates a new instance of a randomly selected prefab at a random position within
    /// specified points and a given y position.
    /// </summary>
    /// <param name="points">A dictionary that contains the minimum and maximum values for the X and Z
    /// coordinates. The keys in the dictionary are "minX", "maxX", "minZ", and "maxZ", and the
    /// corresponding values are floats.</param>
    /// <param name="prefabs">A list of GameObjects that represent different prefabs that can be
    /// instantiated.</param>
    /// <param name="yPosition">The yPosition parameter is the desired y-coordinate of the instantiated
    /// object. It determines at what height the object will be placed in the scene.</param>
    protected void NewInstance(Dictionary<string,float> points, List<GameObject> prefabs, float yPosition)
    {
        float randomX = Random.Range(points["minX"], points["maxX"]);
        float randomZ = Random.Range(points["minZ"], points["maxZ"]);
        
        Vector3 randomPosition = new Vector3(randomX, yPosition, randomZ);
        int randomPrefabIndex = Random.Range(0, prefabs.Count);
        
        Instantiate(prefabs[randomPrefabIndex], randomPosition, prefabs[0].transform.rotation);
    }
    
    
    /// <summary>
    /// The function FindSpawnPoints returns a dictionary containing the maximum and minimum x and z
    /// coordinates of a list of spawn points.
    /// </summary>
    /// <returns>
    /// A dictionary is being returned.
    /// </returns>
    protected Dictionary<string, float> FindSpawnPoints()
    {
        return new Dictionary<string, float> {
            {"maxX", spawnPoints.Select(x => x.position.x).Max()}, 
            {"minX", spawnPoints.Select(x => x.position.x).Min()}, 
            {"maxZ", spawnPoints.Select(x => x.position.z).Max()}, 
            {"minZ", spawnPoints.Select(x => x.position.z).Min()}
        };
    }
    #endregion
   
    #region abstract Methods
    protected abstract IEnumerator SpawnWithDelay(int time, int amount = 1);

    #endregion

}