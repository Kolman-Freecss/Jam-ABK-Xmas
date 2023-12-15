using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audible : MonoBehaviour
{
    [SerializeField] float range = 10f;
    [SerializeField] float emmisionFrequency = 5f;
    [SerializeField] float speedThresholdToEmit = 3f;

    [SerializeField] string allegiance;

    float lastEmissionTime;
    Vector3 lastPosition;

    private void Start() 
    {
        lastEmissionTime = Time.time + Random.Range(0f, 1f / emmisionFrequency);
        lastPosition = transform.position;
    }

    private void Update() 
    {
        //Velocidad = Distancia / Tiempo
        float currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;

        if (currentSpeed >= speedThresholdToEmit)
        {
            if (Time.time - lastEmissionTime > (1f / emmisionFrequency))
            {
                lastEmissionTime = Time.time;
                Emit();
            }
            lastPosition = transform.position;
        }
    }

    private void Emit()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, range);
        foreach (Collider c in colls)
        {
            if (c.TryGetComponent<EntityAudition>(out EntityAudition entityAudition))
            {
                entityAudition.NotifyAudible(this);
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public string GetAllegiance() {return allegiance;}
}
