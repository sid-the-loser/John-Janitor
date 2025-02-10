using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBehaviour : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float attackDamage;
    
    private float _health;
    
    private bool _isDead;

    private void Start()
    {
        _health = maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(float value)
    {
        _health = value;
    }

    public float GetHealth()
    {
        return _health;
    }

    public void DamageHealth(float value)
    {
        _health -= value;

        if (_health <= 0)
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
