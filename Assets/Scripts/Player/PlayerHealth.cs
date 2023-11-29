using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int life;
    public void TakeDamage(int damage)
    {
        life -= damage;
    }
}
