using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleAnimation : MonoBehaviour
{
    // Start is called before the first frame update'
    public Vector3 startPos;
    public Vector3 size;

  
    private void OnEnable()
    {
        gameObject.transform.DOShakeScale(0.5f,0.2f,2, 0,false).SetLoops(-1, LoopType.Yoyo);
        //  gameObject.transform.DOScale(size, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    

    
}
    