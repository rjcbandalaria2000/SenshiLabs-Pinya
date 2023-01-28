using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class DisplayMinigameResult : MonoBehaviour
{
    public GameObject MotivationMeter;
    public GameObject PinyaMeter;

    public  Slider motivationMeterSlider;
    public Slider pinyaMeterSlider;

    private PlayerData playerData;

    private float currentVel = 0;
    // Start is called before the first frame update
    void Start()
    {        
        Assert.IsNotNull(MotivationMeter, "Motivation meter on result screen is null or not set");
        Assert.IsNotNull(PinyaMeter, "Pinya meter on result screen is null or not set");
        motivationMeterSlider = MotivationMeter.GetComponent<Slider>();
        pinyaMeterSlider = PinyaMeter.GetComponent<Slider>();   
    }

    public void DisplayMotivation()
    {
        playerData = SingletonManager.Get<PlayerData>();
        Assert.IsNotNull(motivationMeterSlider);
        Assert.IsNotNull(playerData);
        
        if (motivationMeterSlider == null) { return; }
        if(playerData == null) { return; }
        motivationMeterSlider.maxValue = playerData.maxMotivationData;

        motivationMeterSlider.value = playerData.previousStoredMotivation;
        motivationMeterSlider.DOValue(playerData.storedMotivationData, 1f);

       // motivationMeterSlider.value = playerData.storedMotivationData;
        Debug.Log("Remaining motivation");
    }

    public void DisplayPinyaMeter()
    {
        playerData = SingletonManager.Get<PlayerData>();
        Assert.IsNotNull(pinyaMeterSlider);
        Assert.IsNotNull(playerData);
        if (pinyaMeterSlider == null) { return; }
        if (playerData == null) { return; }
        pinyaMeterSlider.maxValue = playerData.maxPinyaData;
        pinyaMeterSlider.value = playerData.storedPinyaData;
        Debug.Log("Remaining pinya");
    }

}
