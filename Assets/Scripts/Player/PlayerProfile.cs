using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile 
{
    public float motivationMeter;
    public float piñyaMeter;

    public PlayerProfile (Player player)
    {
        motivationMeter = player.motivationMeter.MotivationAmount;
        piñyaMeter = player.pinyaMeter.PinyaValue;
    }
}
