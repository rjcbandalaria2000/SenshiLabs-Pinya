using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ButtonsAnimation : MonoBehaviour
{
    public List<GameObject> buttons;
    // Start is called before the first frame update

    IEnumerator scaleAnimation()
    {
        foreach(var list in buttons)
        {
            list.transform.DOScale(0.6418479f, 1f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(1f);
       // PlayAnimation();
    }

    public void PlayAnimation()
    {
        StartCoroutine(scaleAnimation());
    }

}
