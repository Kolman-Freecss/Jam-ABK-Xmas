using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTransition : MonoBehaviour
{
    [SerializeField]
    Animator transition;

    [SerializeField]
    float transitionTime = 1f;

    public HouseTransition() { }

    IEnumerator Transition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        //TODO: Teleport to house
    }
}
