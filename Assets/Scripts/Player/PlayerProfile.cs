using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile 
{
    public float motivationMeter;
    public float pinyaMeter;

    public PlayerProfile (Player player)
    {
        motivationMeter = player.motivationMeter.MotivationAmount;
        pinyaMeter = player.pinyaMeter.pinyaValue;
    }
}
