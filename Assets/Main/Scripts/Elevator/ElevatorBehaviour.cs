using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour
{
    [SerializeField] private Animator door1Animator;
    [SerializeField] private Animator door2Animator;

    private bool _firstContact = true;
    private bool _lastContact;

    public void TriggerElevator()
    {
        if (!_firstContact)
        {
            door1Animator.SetTrigger("Hey");
            door2Animator.SetTrigger("Hey");
        }
        else if (_lastContact)
        {
            
        }
        else
        {
            _firstContact = false;
        }
    }

    public void SetLastContact(bool value)
    {
        _lastContact = value;
    }
}
