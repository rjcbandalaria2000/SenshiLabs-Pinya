using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ThresholdUX : MonoBehaviour
{
    // Start is called before the first frame update
    public float coolDown;
    public Slider thresholdSlider;
    public float value;
    private void Awake()
    {
        thresholdSlider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        ActivateThreshold(value);
    }
    public IEnumerator PulsateObj()
    {
       float tempCooldown = coolDown;
        //  crackedGlass.SetActive(true);
        //  overlayGO.SetActive(true);
        while (tempCooldown > 0)
        {
            Debug.Log("animatio");
            gameObject.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);
            // overlayGO.transform.DOScale(1.1f, 0.3f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(1f);
            gameObject.transform.DOScale(4, 0.3f).SetEase(Ease.OutBounce);
            //overlayGO.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBounce);
            //    crackedGlass.SetActive(false);
         //   tempCooldown--;

        }
        //  heartGO.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);

        gameObject.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);
        //  overlayGO.SetActive(false);
        Debug.Log("End Pulsate");
    }

    public void ActivateThreshold(float value)
    {
        if(thresholdSlider.value <= value)
        {
            StartCoroutine(PulsateObj());
        }
    }
}
