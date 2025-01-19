using System;
using System.Collections;
using System.Collections.Generic;
using Sid.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected enum States
    {
        Wander,
        Attack,
        Die
    }

    protected NavMeshAgent Agent;
    
    protected Vector3 TargetPosition;
    
    protected GameObject PlayerObject;
    
    protected States CurrentState = States.Wander;
    
    protected bool AttackTriggered = false;

    protected void OnStart()
    {
        TargetPosition = transform.position;
        Agent = GetComponent<NavMeshAgent>();
        PlayerObject = FindObjectOfType<PlayerMovement>().gameObject;
    }

    protected void LateUpdate()
    {
        if (!AttackTriggered)
        {
            if (Vector3.Distance(PlayerObject.transform.position, transform.position) <= 10f)
            {
                CurrentState = States.Attack;
                AttackTriggered = true;
            }
        }
        
        Agent.SetDestination(TargetPosition);
    }
}
