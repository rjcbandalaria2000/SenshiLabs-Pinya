using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MotivationMeter  motivationMeter;
    public PinyaMeter       pinyaMeter;

    private void Start()
    {
        //Events.OnSceneChange.AddListener(SavePlayer);
        //SavePlayer();
        //LoadPlayer();
        //Events.OnSceneLoad.AddListener(LoadPlayer);
    }

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

    //public void RemoveListeners()
    //{
    //    Events.OnSceneChange.RemoveListener(SavePlayer);
    //   // Events.OnSceneLoad.RemoveListener(LoadPlayer);
    //}
}
