using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
public class DisplayMinigameResult : MonoBehaviour
{
    public GameObject MotivationMeter;
    public GameObject PinyaMeter;

    private Slider motivationMeterSlider;
    private Slider pinyaMeterSlider;

    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        if(playerData == null) 
        {
            playerData = SingletonManager.Get<PlayerData>();
        }
        Assert.IsNotNull(MotivationMeter, "Motivation meter on result screen is null or not set");
        Assert.IsNotNull(PinyaMeter, "Pinya meter on result screen is null or not set");
        motivationMeterSlider = MotivationMeter.GetComponent<Slider>();
        pinyaMeterSlider = PinyaMeter.GetComponent<Slider>();   
    }

    public void DisplayMotivation()
    {
        if(motivationMeterSlider == null) { return; }
        if(playerData == null) { return; }
        motivationMeterSlider.maxValue = playerData.maxMotivationData;
        motivationMeterSlider.value = playerData.storedMotivationData;
    }

    public void DisplayPinyaMeter()
    {
        if (pinyaMeterSlider == null) { return; }
        if (playerData == null) { return; }
        pinyaMeterSlider.maxValue = playerData.maxPinyaData;
        pinyaMeterSlider.value = playerData.storedPinyaData;
    }
   
}
