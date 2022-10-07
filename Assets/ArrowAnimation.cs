using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowAnimation : MonoBehaviour
{
    public Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        PlayAnimation();
    }



    public void PlayAnimation()
    {
        transform.DOLocalMove(endPos, 1).SetLoops(-1, LoopType.Yoyo);
    }


}
