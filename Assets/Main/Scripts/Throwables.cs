using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwables : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            name = this.name;
            if (name == "Chair")
            {
                //add monster health remove here
                Debug.Log("hit enemy");
            }
        }
    }
}
