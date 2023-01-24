using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowAnimation : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    private Tweener arrowAnimation;

    // Start is called before the first frame update
    void Start()
    {
        arrowAnimation = null;
    }

    private void OnEnable()
    {
        PlayAnimation();
    }

    private void OnDisable()
    {
        
        ResetPosition();
        Debug.Log("Disabled Arrow");
    }

    public void PlayAnimation()
    {
        arrowAnimation = transform.DOLocalMove(endPos, 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void ResetPosition()
    {

        arrowAnimation.Kill();
        Debug.Log("Killing arrow animation");

        this.transform.position = startPos;
        Debug.Log("Reseting Position");
    }

}
