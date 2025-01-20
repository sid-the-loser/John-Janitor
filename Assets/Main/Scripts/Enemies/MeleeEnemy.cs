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
    private bool animatorTriggered;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        OnStart();
    }

    private void Update()
    {
        if (CurrentState == States.Attack)
        {
            if (!animatorTriggered)
            {
                animator.SetTrigger("Moving");
                animatorTriggered = true;
            }

            TargetPosition = Vector3.Distance(PlayerObject.transform.position, transform.position) > 2f
                ? PlayerObject.transform.position
                : transform.position;
        }
    }
}
