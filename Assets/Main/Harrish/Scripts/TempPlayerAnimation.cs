using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerAnimation : MonoBehaviour
{
    public Animator animator;

    public bool isJogging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isJogging", isJogging);
        
        
    }
}
