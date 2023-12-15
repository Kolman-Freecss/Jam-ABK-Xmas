using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAudition : MonoBehaviour
{
    [Serializable]
    public class AudibleHeard
    {
        public Audible audible;
        public float timeLeftToForget = 5f;
        internal string GetAllegiance()
        {
            return audible.GetAllegiance();
        }
    }

    public List<AudibleHeard> heardAudibles = new();

    private void Update() 
    {
        foreach (AudibleHeard ah in heardAudibles)
            ah.timeLeftToForget -= Time.deltaTime;
        
        //borra todos aquellos que cumplan que sean menores o iguales a 0
        heardAudibles.RemoveAll(x => x.timeLeftToForget <= 0f);

        heardAudibles.Sort(
            (ah1, ah2) => 
            (Vector3.Distance(ah1.audible.transform.position, transform.position) < 
            Vector3.Distance(ah2.audible.transform.position, transform.position)) ? 1 : 0);
    }

    public void NotifyAudible(Audible audible)
    {
        AudibleHeard existingAudibleHeard = heardAudibles.Find(x => x.audible == audible);

        if (existingAudibleHeard != null)
            existingAudibleHeard.timeLeftToForget = 5f;
        else
        {
            AudibleHeard audibleHeard = new AudibleHeard();
            audibleHeard.audible = audible;
            heardAudibles.Add(audibleHeard);
        }
    }

    public Transform GetBestTarget()
    {
        return heardAudibles.Count > 0 ? heardAudibles[0].audible.transform : null;
    }
}
