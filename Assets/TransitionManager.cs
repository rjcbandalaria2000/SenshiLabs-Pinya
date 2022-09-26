using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TransitionManager : MonoBehaviour
{
    public GameObject   curtain;
    public Animator     animator;
    public float        transitionTime;

    //Animation Names
    public const string CURTAIN_OPEN = "CurtainsOpening";
    public const string CURTAIN_CLOSE = "CurtainsClosing";

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

    public bool IsAnimationFinished() {
        if(animator == null) { return false; }
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= transitionTime;
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
