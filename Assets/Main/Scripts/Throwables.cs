using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwables : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (this.CompareTag("canLiftHeavy"))
            { 
                if (rb.velocity.magnitude > 10)
                {
                    other.gameObject.GetComponent<StatsBehaviour>().DamageHealth(2);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
