using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnlyWal : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }
}
