using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sound.Scripts
{
    public class LookingAtObject : MonoBehaviour
    {
        [SerializeField] private List<GameObject> children = new List<GameObject>();

        

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Interactable Object"))
            {
                children.Clear();
                foreach (Transform child in other.transform)
                {
                    children.Add(child.gameObject);
                }

                foreach (var child in children)
                {
                    var shader = child.GetComponent<Renderer>().material.shader;
                    if (shader != null)
                    {
                        child.GetComponent<Renderer>().material.SetFloat("_ActiveRim", 1);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            foreach (var child in children)
            {
                var shader = child.GetComponent<Renderer>().material.shader;
                if (shader != null)
                {
                    child.GetComponent<Renderer>().material.SetFloat("_ActiveRim", 0);
                }
            }
        }
    }
}