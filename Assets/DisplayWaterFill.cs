using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayWaterFill : MonoBehaviour
{
    public FillWaterBucket fillWaterBucket;
    public Slider waterSlider; 
    // Start is called before the first frame update
    void Start()
    {
        waterSlider = this.GetComponent<Slider>(); 
    }

    public void Initialize()
    {
        waterSlider.maxValue = fillWaterBucket.maxWater;
        waterSlider.value = fillWaterBucket.waterAmount;
    }

    public void UpdateWaterFill()
    {
        waterSlider.value = fillWaterBucket.waterAmount;
    }



   
}
