using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DisplayWaterFill : MonoBehaviour
{
    [Header("References")]
    public FillWaterBucket  fillWaterBucket;
    public Slider           waterSlider;

    [Header("Tween Animation")]
    public float            animationDuration;

    // Start is called before the first frame update
    void Start()
    {
        waterSlider = this.GetComponent<Slider>();
        Initialize();
        Events.OnWaterFilling.AddListener(UpdateWaterFill);
    }

    public void Initialize()
    {
        waterSlider.maxValue = fillWaterBucket.maxWater;
        waterSlider.value = fillWaterBucket.waterAmount;
    }

    public void UpdateWaterFill()
    {
        waterSlider.DOValue(fillWaterBucket.waterAmount, 1, false);
        //waterSlider.value = fillWaterBucket.waterAmount;
    }



   
}
