using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class LoadingTextAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI text;
    public string loadingText;
    public Vector3 jumpPower;
    public float speed;
    void Start()
    {
        StartCoroutine(AnimateText());
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public IEnumerator AnimateText()
    {
        while (true)
        {
            foreach (char letter in loadingText.ToCharArray())
            {
              //  text.transform.DOFlip();

                text.text += letter;
               // text.transform.DOLocalJump(jumpPower, 10, 1, 0.3f);
                yield return new WaitForSeconds(speed);
                //text.transform.DOFlip();
            }
            text.text = "";
        }
        
    }
   
}
