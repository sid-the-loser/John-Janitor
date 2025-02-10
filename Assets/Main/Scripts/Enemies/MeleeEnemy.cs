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
    private StatsBehaviour stats;

    private bool primed;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<StatsBehaviour>();
        OnStart();
    }

    private void Update()
    {
        if (stats.GetIsDead())
        {
            Destroy(gameObject);
        }
        
        if (CurrentState == States.Moving)
        {

            if (!primed && Vector3.Distance(PlayerObject.transform.position, transform.position) > 2f)
            {
                animator.SetTrigger("Moving");
                TargetPosition = PlayerObject.transform.position;
            }
            else if (!primed)
            {
                animator.SetTrigger("Explode");
                TargetPosition = transform.position;
                StartCoroutine(Explode());
            }
        }
    }

    private IEnumerator Explode()
    {
        if (!primed)
        {
            primed = true;
            yield return new WaitForSeconds(0.5f);
            if (Vector3.Distance(PlayerObject.transform.position, transform.position) <= 2f)
            {
                PlayerObject.GetComponent<StatsBehaviour>().DamageHealth(stats.GetAttackDamage());
                Debug.Log("damaged");
            }
            Destroy(this.gameObject);
        }
            
    }
}
