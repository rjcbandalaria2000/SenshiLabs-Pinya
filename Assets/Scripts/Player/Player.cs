using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MotivationMeter  motivationMeter;
    public PinyaMeter       pinyaMeter;

    private void Start()
    {
        if(motivationMeter == null)
        {
            motivationMeter = this.GetComponent<MotivationMeter>();
        }
        if(pinyaMeter == null)
        {
            pinyaMeter = this.GetComponent<PinyaMeter>();
        }
        
        Events.OnSceneChange.AddListener(OnSceneChange);
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
        pinyaMeter.PinyaValue = data.pinyaMeter;

        SingletonManager.Get<DisplayMotivationalBar>().UpdateMotivationBar();
        SingletonManager.Get<DisplayPinyaMeter>().UpdatePinyaBar();
    }

    public void OnSceneChange()
    {
        if(motivationMeter == null) { return; }
        if(pinyaMeter == null) { return; }
        SingletonManager.Get<Player_Data>().storedMotivationData = motivationMeter.MotivationAmount;
        SingletonManager.Get<Player_Data>().storedPinyaData = pinyaMeter.PinyaValue;
        if (!SingletonManager.Get<Player_Data>().HasSaved)
        {
            SingletonManager.Get<Player_Data>().HasSaved = true;
        }
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }

    //public void RemoveListeners()
    //{
    //    Events.OnSceneChange.RemoveListener(SavePlayer);
    //   // Events.OnSceneLoad.RemoveListener(LoadPlayer);
    //}
}
