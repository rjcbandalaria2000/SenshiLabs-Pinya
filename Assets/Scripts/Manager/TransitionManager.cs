using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransitionManager : MonoBehaviour
{
    public GameObject   curtain;
    public Animator     animator;
    public string       currentAnimation;
    [Range(0f, 1f)]
    public float        transitionTime = 1;

    public float animationTime;

    //Animation Names
    public const string CURTAIN_OPEN = "CurtainsOpening";
    public const string CURTAIN_CLOSE = "CurtainsClosing";
    public const string CURTAIN_IDLE = "Idle";

    private Coroutine openingTransitionRoutine;


    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartOpeningTransition();
        //ChangeAnimation(CURTAIN_OPEN);
    }

    public void ChangeAnimation(string animationName)
    {
        if (animator == null) { return; }
        animator.Play(animationName, 0 ,0f);
        currentAnimation = animationName;
    }
   
    public void Initialize()
    {
        
    }

    public float GetAnimationLength()
    {
        if(animator == null) { return 0f; }
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public bool IsAnimationFinished() {
        if(animator == null) { return false; }
        //Check if the active animation playing is the current animation set
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation))
        {
            //Debug.Log("Correct animation");
            //Debug.Log("Animation time: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            //animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation);
            if (animator.GetCurrentAnimatorStateInfo(0).speed >= 1) // if the speed is positive 
            {
                return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= (transitionTime);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).speed <= 0) // if speed is negative. used in closing animation since the speed is -1 to reverse the animation
            {
                return animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animator.GetCurrentAnimatorStateInfo(0).speed <= (transitionTime * animator.GetCurrentAnimatorStateInfo(0).speed);
            }

        }
        return false;
    }

    public void OnSceneChange()
    {

    }

    public void StartOpeningTransition()
    {
        openingTransitionRoutine = StartCoroutine(OpeningTransition());
    }

    IEnumerator OpeningTransition()
    {
        Events.OnCurtainStart.Invoke();
        ChangeAnimation(CURTAIN_OPEN);

        while (!IsAnimationFinished())
        {
            yield return null; 
        }

        yield return null; 
        Events.OnCurtainsOpened.Invoke();
    }

}
