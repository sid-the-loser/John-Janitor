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
        Moving,
        Attack,
        Die
    }

    protected NavMeshAgent Agent;
    
    protected Vector3 TargetPosition;

    private Vector3 pastTergetPOsition;
    
    protected GameObject PlayerObject;
    
    protected States CurrentState = States.Wander;
    
    protected bool MovingTriggered = false;

    protected void OnStart()
    {
        TargetPosition = transform.position;
        Agent = GetComponent<NavMeshAgent>();
        PlayerObject = FindObjectOfType<PlayerMovement>().gameObject;
    }

    protected void LateUpdate()
    {
        if (!MovingTriggered)
        {
            if (Vector3.Distance(PlayerObject.transform.position, transform.position) <= 10f)
            {
                CurrentState = States.Moving;
                MovingTriggered = true;
            }
        }

        if (pastTergetPOsition != TargetPosition)
        {
            Agent.SetDestination(TargetPosition);
            pastTergetPOsition = TargetPosition;
        }
    }
}
