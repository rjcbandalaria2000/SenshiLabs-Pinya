using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    public float storedMotivationData;
    public float storedPinyaData;

    [Header("Pre-Requisite")]
    public bool achieveGetWater;
    public bool achieveGroceryTask;

    public void Awake()
    {
        SingletonManager.Register(this);
    }

   
    public void storeData(Player player)
    {
        storedMotivationData = player.motivationMeter.MotivationAmount;
        storedPinyaData = player.pinyaMeter.PinyaValue;
    }

    public void getData(Player player)
    {
        player.motivationMeter.MotivationAmount = storedMotivationData;
        player.pinyaMeter.PinyaValue = storedPinyaData;
    }

    public bool getAchieveGetWater()
    {
        return achieveGetWater;
    }

    public bool getAchieveGroceryTask()
    {
        return achieveGroceryTask;
    }
}
