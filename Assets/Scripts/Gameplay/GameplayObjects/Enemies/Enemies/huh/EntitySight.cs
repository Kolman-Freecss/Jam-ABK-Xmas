 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySight : MonoBehaviour
{
    [SerializeField] Vector3 sightSize = new Vector3(20f, 30f, 30f); 
    [SerializeField] Vector2 sightAngles = new Vector2(60f, 45f);
    [SerializeField] float frequency = 10f; //times per second

    [SerializeField] LayerMask isVisible = Physics.DefaultRaycastLayers;
    [SerializeField] LayerMask isOclusion = Physics.DefaultRaycastLayers;

    public List<IVisible> visiblesInSight = new();
    float lastSightCheckTime = 0f;

    private void Start() 
    {
        lastSightCheckTime = Time.time;
        lastSightCheckTime += Random.Range(0f, 1f / frequency);
    }

    private void Update() 
    {
        if ((Time.time - lastSightCheckTime) > (1f / frequency))
        {
            //check sight
            lastSightCheckTime += 1f / frequency;

            visiblesInSight.Clear();
            
            Collider[] colls = Physics.OverlapBox(transform.position + transform.forward * sightSize.z / 2f, 
            sightSize / 2f, transform.rotation, isVisible);

            for (int i = 0; i < colls.Length; i++)
            {
                Vector3 direction = colls[i].transform.position;
                float horizontalAngles = Vector3.SignedAngle(transform.forward, direction, transform.up);
                float verticalAngles = Vector3.SignedAngle(transform.forward, direction, transform.right);

                if (Mathf.Abs(horizontalAngles) < sightAngles.x && Mathf.Abs(verticalAngles) < sightAngles.y)
                {
                    if (Physics.Raycast(transform.position, direction, out RaycastHit hit, sightSize.z, isOclusion))
                    {
                        if (colls[i] = hit.collider)
                        {
                            IVisible visible = colls[i].GetComponent<IVisible>();
                            if (visible != null)
                                visiblesInSight.Add(visible);
                        }
                    }
                }
            }
        }
    }
}
