using System;
using System.Collections;
using System.Collections.Generic;
using Sid.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MeleeEnemy : EnemyBase
{
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        OnStart();
    }

    private void Update()
    {
        if (CurrentState == States.Moving)
        {

            if (Vector3.Distance(PlayerObject.transform.position, transform.position) > 2f)
            {
                animator.SetTrigger("Moving");
                TargetPosition = PlayerObject.transform.position;
            }
            else
            {
                animator.SetTrigger("Idle");
                TargetPosition = transform.position;
            }
        }
    }
}
