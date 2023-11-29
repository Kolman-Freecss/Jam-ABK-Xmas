using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    //used in its heritage
    public abstract EnemyState RunCurrentState();
}
