using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransitionManager : MonoBehaviour
{
    public GameObject curtain;
    public Animator animator;

    //Animation Names
    public const string CURTAIN_OPEN = "CurtainsOpening";
    public const string CURTAIN_CLOSE = "CurtainsClosing";

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnimation(CURTAIN_CLOSE);
    }

    public void ChangeAnimation(string animationName)
    {
        if (animator == null) { return; }
        animator.Play(animationName);
    }
   
    public void Initialize()
    {
        
    }

    public float GetAnimationLength()
    {
        if(animator == null) { return 0f; }
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

}
