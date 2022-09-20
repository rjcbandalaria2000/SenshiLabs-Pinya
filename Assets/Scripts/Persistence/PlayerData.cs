using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Player")]
    public float    storedMotivationData;
    public float    storedPinyaData;
    public int      MinigamesPlayed;
    public bool     HasSaved;

    [Header("Minigames")]
    public MinigameObject ActivatedMinigame; 
    public bool     IsCleanTheHouseFinished;
    public bool     IsWashTheDishesFinished;
    public bool     IsGroceryFinished;
    public bool     IsWaterThePlantsFinished;
    public bool     IsGetWaterFinished;
    public bool     IsImHungryFinished;

    public void Awake()
    {
        SingletonManager.Register(this);
    }

   
    public void StoreData(Player player)
    {
        storedMotivationData = player.motivationMeter.MotivationAmount;
        storedPinyaData = player.pinyaMeter.PinyaValue;
    }

    public void GetData(Player player)
    {
        player.motivationMeter.MotivationAmount = storedMotivationData;
        player.pinyaMeter.PinyaValue = storedPinyaData;
    }
}
