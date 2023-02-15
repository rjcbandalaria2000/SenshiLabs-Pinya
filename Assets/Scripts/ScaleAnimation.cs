using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleAnimation : MonoBehaviour
{
    // Start is called before the first frame update'
    public Vector3 startPos;
    public Vector3 size;
   // public bool manualSize;

  
    private void OnEnable()
    {
        gameObject.transform.DOShakeScale(1f, 0.1f, 0, 0, true).SetLoops(-1, LoopType.Yoyo);

        //if (!manualSize)
        //{

        //}
        //else
        //{
        //    gameObject.transform.DOScale(size, 1f).SetLoops(-1, LoopType.Yoyo);
        //    //gameObject.transform.DOShakeScale(1f, 0.1f, 0, 0, true).SetLoops(-1, LoopType.Yoyo);
        //}
    }

 




}
    