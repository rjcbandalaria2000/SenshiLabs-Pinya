using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OpeningAnimationBehavior : StateMachineBehaviour
{
    public bool isFinished = false;
    public float openedTime = 1;
    public GameObject parent; 
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parent = animator.gameObject.GetComponent<UnitInfo>().Parent;
        Events.OnCurtainStart.Invoke();
        //SingletonManager.Get<UIManager>().DeactivateGameUI();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Animation Length: " + stateInfo.length);
        //Debug.Log("Animation Normalized Time: " + stateInfo.normalizedTime);
        if (stateInfo.normalizedTime >= openedTime)
        {
            if (!isFinished)
            {
                Debug.Log("Finished Animation");
                isFinished = true;
                Assert.IsNotNull(parent, "Curtain not set or is null");
                parent.SetActive(false);
                Events.OnCurtainsOpened.Invoke();
            }
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
