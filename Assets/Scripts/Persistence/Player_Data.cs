using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    public float storedMotivationData;
    public float storedPinyaData;

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
}
