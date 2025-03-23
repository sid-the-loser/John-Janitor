using System;
using System.Collections;
using System.Collections.Generic;
using Sid.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float visionTriggerDistance = 20f;
    [SerializeField] protected NavMeshAgent Agent;
    
    protected enum States
    {
        Wander,
        Moving,
        Attack,
        Die
    }
    
    protected Vector3 TargetPosition;

    private Vector3 pastTargetPosition;
    
    protected static GameObject PlayerObject;
    
    protected States CurrentState = States.Wander;
    
    protected bool MovingTriggered = false;

    protected void OnStart()
    {
        TargetPosition = transform.position;
        // Agent = GetComponent<NavMeshAgent>();
        
        if (PlayerObject is null)
        {
            PlayerObject = FindObjectOfType<PlayerMovement>().gameObject;
        }
    }

    protected void LateUpdate()
    {
        if (!MovingTriggered)
        {
            if (Vector3.Distance(PlayerObject.transform.position, transform.position) <= visionTriggerDistance)
            {
                CurrentState = States.Moving;
                MovingTriggered = true;
            }
        }

        if (pastTargetPosition != TargetPosition)
        {
            Agent.SetDestination(TargetPosition);
            pastTargetPosition = TargetPosition;
        }
    }
}
