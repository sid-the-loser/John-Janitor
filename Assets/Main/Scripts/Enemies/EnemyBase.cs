using System;
using System.Collections;
using System.Collections.Generic;
using Sid.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float visionTriggerDistance = 20f;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float secondsToDoExplodeEffect = 2.0f;
    
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
    
    public static int InSceneCount;
    
    protected States CurrentState = States.Wander;
    
    protected bool MovingTriggered = false;

    protected void OnWake()
    {
        InSceneCount++;
    }

    protected void OnStart()
    {
        TargetPosition = transform.position;
        // Agent = GetComponent<NavMeshAgent>();
        
        PlayerObject = FindObjectOfType<PlayerMovement>().gameObject;
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
            agent.SetDestination(TargetPosition);
            pastTargetPosition = TargetPosition;
        }
    }

    protected void OnDestroy()
    {
        InSceneCount--;
    }
    
    public IEnumerator Die()
    {
        var effect = Instantiate(explosionEffect, transform);
        Destroy(this);
        yield return new WaitForSeconds(secondsToDoExplodeEffect);
        Destroy(effect);
    }
}
