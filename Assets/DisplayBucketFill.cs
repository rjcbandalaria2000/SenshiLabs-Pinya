using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DisplayBucketFill : MonoBehaviour
{
    public FillWaterBucket  waterBucket;
    public Image            waterBucketFill;

    [Header("Tween Animation")]
    public float            duration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Events.OnWaterFilling.AddListener(UpdateWaterFill);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void UpdateWaterFill()
    {
        if(waterBucketFill == null) { return; }
        if(waterBucket == null) { return; }
        //waterBucketFill.fillAmount = waterBucket.GetNormalizedWaterAmount();
        waterBucketFill.DOFillAmount(waterBucket.GetNormalizedWaterAmount(), duration);
    }

    public void UpdateWaterFill(float amount)
    {
        if(waterBucketFill == null) { return; }
        waterBucketFill.DOFillAmount(amount, duration);
    }
    
    public void OnSceneChange()
    {
        Events.OnWaterFilling.RemoveListener(UpdateWaterFill);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
