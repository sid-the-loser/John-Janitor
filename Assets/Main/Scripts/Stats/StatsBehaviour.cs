using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBehaviour : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float attackDamage;
    
    private bool _isDead;

    public void SetHealth(float value)
    {
        health = value;
    }

    public float GetHealth()
    {
        return health;
    }

    public void DamageHealth(float value)
    {
        health -= value;

        if (health <= 0)
        {
            _isDead = true;
        }
    }
    
    public void SetSpeed(float value)
    {
        speed = value;
    }

    public float GetSpeed()
    {
        return speed;
    }
    
    public void SetAttackDamage(float value)
    {
        attackDamage = value;
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    public bool GetIsDead()
    {
        return _isDead;
    }
}
