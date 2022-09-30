using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MotivationMeter  motivationMeter;
    public PinyaMeter       pinyaMeter;

    private PlayerData playerData;

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
        pinyaMeter.pinyaValue = data.pinyaMeter;

        SingletonManager.Get<DisplayMotivationalBar>().UpdateMotivationBar();
        SingletonManager.Get<DisplayPinyaMeter>().UpdatePinyaBar();
    }

    public void OnSceneChange()
    {
        if(motivationMeter == null) { return; }
        if(pinyaMeter == null) { return; }
        if (SingletonManager.Get<PlayerData>())
        {
            SingletonManager.Get<PlayerData>().StoreData(this);
            if (!SingletonManager.Get<PlayerData>().HasSaved)
            {
                SingletonManager.Get<PlayerData>().HasSaved = true;
            }
        }
      
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }

    //public void RemoveListeners()
    //{
    //    Events.OnSceneChange.RemoveListener(SavePlayer);
    //   // Events.OnSceneLoad.RemoveListener(LoadPlayer);
    //}
}
