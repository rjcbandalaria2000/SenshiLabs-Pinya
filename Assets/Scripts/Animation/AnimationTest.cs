using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("IsIdle",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
