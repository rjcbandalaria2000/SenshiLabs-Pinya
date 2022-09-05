using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile 
{
    public float motivationMeter;
    public float pi�yaMeter;

    public PlayerProfile (Player player)
    {
        motivationMeter = player.motivationMeter.MotivationAmount;
        pi�yaMeter = player.pinyaMeter.PinyaValue;
    }
}
