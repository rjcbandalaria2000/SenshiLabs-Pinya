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
    public int                      MinigamesPlayed;
    public bool                     HasSaved;
    public Vector2                  playerLocation;

    [Header("Minigames")]
    
    public bool                     IsCleanTheHouseFinished;
    public bool                     IsWashTheDishesFinished;
    public bool                     IsGroceryFinished;
    public bool                     IsWaterThePlantsFinished;
    public bool                     IsGetWaterFinished;
    public bool                     IsImHungryFinished;
    //public bool     IsTagFinished;
    //public bool     IsHideSeekFinished;
    //public bool     IsSleepFinished;

    [Header("Tasks")]
    public List<string>     requiredTasks;

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
        MinigamesPlayed = 0;
        HasSaved = false;
        playerLocation = Vector2.zero;
        IsCleanTheHouseFinished = false;
        IsGetWaterFinished = false;
        IsGroceryFinished = false;
        IsImHungryFinished = false;
        IsWashTheDishesFinished = false;
        IsWaterThePlantsFinished = false;
        requiredTasks.Clear();
    }
}
