using System;
using System.Collections;
using System.Collections.Generic;
using Sid.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MeleeEnemy : EnemyBase
{
    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case States.Attack:
                TargetPosition = Vector3.Distance(PlayerObject.transform.position, transform.position) > 2f ? PlayerObject.transform.position : transform.position;
                break;
            case States.Wander:
            {
                TargetPosition = transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                break;
            }
        }
    }
}
