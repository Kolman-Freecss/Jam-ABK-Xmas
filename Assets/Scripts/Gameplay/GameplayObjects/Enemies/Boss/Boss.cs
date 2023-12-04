using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Transform player;
    public bool isFlipped = false;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
