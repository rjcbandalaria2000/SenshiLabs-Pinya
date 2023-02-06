using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DisplayLowMotivation : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 originalPosition;
    public Vector2 endPosition;
    public RectTransform rectTransform;
    public TextMeshProUGUI lowMotivationText;
    private void Awake()
    {
      
    }
    void Start()
    {
      //  originalPosition = rectTransform.anchoredPosition;
      //  rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    StartCoroutine(MoveTransition());
        //}
    }

    private void OnEnable()
    {
        //MoveTransition();
        StartCoroutine(MoveTransition());
    }

    private void OnDisable()
    {
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        transform.gameObject.GetComponent<Image>().DOFade(255, 0.1f);
        lowMotivationText.DOFade(255, 0.1f);
        rectTransform.anchoredPosition = originalPosition;
    }


    public IEnumerator MoveTransition()
    {
         
    //    transform.gameObject.GetComponent<Image>().DOFade(0, 1f);

        rectTransform.DOAnchorPos(endPosition, 3f).WaitForCompletion();
      ///  transform.gameObject.GetComponent<Image>().DOFade(255, 3f);
        // transform.gameObject.GetComponent<Image>().DOFade(255, 1f);
        yield return new WaitForSeconds(3f);


        gameObject.SetActive(false);
        ResetAnimation();
    }


}
