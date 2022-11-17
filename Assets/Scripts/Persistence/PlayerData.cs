using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Player")]
    public float                    storedMotivationData;
    public float                    maxMotivationData;
    public float                    storedPinyaData;
    public float                    maxPinyaData;
    public int                      minigamesPlayed;
    public bool                     hasSaved;
    public Vector2                  playerLocation;
    public bool                     firstTime;

    [Header("Minigames")]
    
    public bool                     isCleanTheHouseFinished;
    public bool                     isWashTheDishesFinished;
    public bool                     isGroceryFinished;
    public bool                     isWaterThePlantsFinished;
    public bool                     isGetWaterFinished;
    public bool                     isImHungryFinished;
    public bool                     isTagFinished;
    public bool                     isHideSeekFinished;
    public bool                     isSleepFinished;
    public bool                     isFoldingClothesFinished;

    [Header("Tasks")]
    public List<string>             requiredTasks;

    [Header("TimePeriod")]
    public int                      savedTimeIndex;
    public TimePeriod               savedTimePeriod;

    public void Awake()
    {
        SingletonManager.Register(this);
    }

   
    public void StoreData(Player player)
    {
        maxMotivationData = player.motivationMeter.MaxMotivation;
        maxPinyaData = player.pinyaMeter.maxPinyaValue;
        storedMotivationData = player.motivationMeter.MotivationAmount;
        storedPinyaData = player.pinyaMeter.pinyaValue;
        playerLocation = player.gameObject.transform.position;
    }

    public void GetData(Player player)
    {
        player.motivationMeter.MotivationAmount = storedMotivationData;
        player.pinyaMeter.pinyaValue = storedPinyaData;
    }

    public void ResetPlayerData()
    {
        storedMotivationData = 0; 
        storedPinyaData = 0;
        maxMotivationData = 0;
        maxPinyaData = 0;
        minigamesPlayed = 0;
        hasSaved = false;
        playerLocation = Vector2.zero;
        isCleanTheHouseFinished = false;
        isGetWaterFinished = false;
        isGroceryFinished = false;
        isImHungryFinished = false;
        isWashTheDishesFinished = false;
        isWaterThePlantsFinished = false;
        isFoldingClothesFinished = false;
        requiredTasks.Clear();
    }
}
