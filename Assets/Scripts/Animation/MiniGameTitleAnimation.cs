using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MiniGameTitleAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform title;

    public Vector2 startPos;
    public Vector2 endPos;
    ButtonsAnimation buttonsAnimation;
    void Start()
    {
        buttonsAnimation = GetComponent<ButtonsAnimation>();
        TitleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TitleAnimation()
    {
        Sequence mySequence = DOTween.Sequence();
        // Add a movement tween at the beginning
        //   mySequence.Append(title.DOAnchorPos(endPos, 1f, true));
         mySequence.Append(title.DOJumpAnchorPos(endPos, 100f, 4, 1f, false)).OnComplete(buttonsAnimation.PlayAnimation);

      /// title.DOJumpAnchorPos(endPos, 100f, 4, 1f, false).OnComplete(Stuff);
        //  buttonsAnimation.PlayAnimation();
        // Add a rotation tween as soon as the previous one is finished
        //mySequence.Append(transform.DORotate(new Vector3(0, 180, 0), 1));
        // Delay the whole Sequence by 1 second
        //  mySequence.PrependInterval(1);
        // Insert a scale tween for the whole duration of the Sequence
        //  mySequence.Insert(0, transform.DOScale(new Vector3(3, 3, 3), mySequence.Duration()));
    }

    public void Stuff()
    {

    }
}
