using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MotivationMeter motivationMeter;
    public PinyaMeter pinyaMeter;

    //Move to Manager
    public void SavePlayer()
    {
        DataManager.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerProfile data = DataManager.LoadPlayer();

        motivationMeter.MotivationAmount = data.motivationMeter;
        pinyaMeter.PinyaValue = data.piñyaMeter;

        SingletonManager.Get<DisplayMotivationalBar>().UpdateMotivationBar();
        SingletonManager.Get<DisplayPinyaMeter>().UpdatePinyaBar();
    }
}
