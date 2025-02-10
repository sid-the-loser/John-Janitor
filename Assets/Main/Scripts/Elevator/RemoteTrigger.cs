using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteTrigger : MonoBehaviour
{
    [SerializeField] private ElevatorBehaviour elevatorBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) elevatorBehaviour.TriggerElevator();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) elevatorBehaviour.TriggerElevator();
    }
}
