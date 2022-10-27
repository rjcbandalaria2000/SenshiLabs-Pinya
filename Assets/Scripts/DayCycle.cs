using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimePeriod{
    Morning = 0,
    Afternoon = 1,
    Evening = 2,
}

public class DayCycle : MonoBehaviour
{

    public TimePeriod   timePeriod;
    public int          timeIndex = 0;
    //[SerializeField] bool isMorning;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    private void Start()
    {
        if (SingletonManager.Get<PlayerData>())
        {
            if (SingletonManager.Get<PlayerData>().hasSaved) 
            {
                timePeriod = SingletonManager.Get<PlayerData>().savedTimePeriod;
                timeIndex = SingletonManager.Get<PlayerData>().savedTimeIndex;
            }
        }
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void ChangeTimePeriod(int timeIndex)
    {
        Mathf.Clamp(timeIndex, 0, 2);
        switch (timeIndex)
        {
            case 0:
                timePeriod = TimePeriod.Morning;
                break;
            case 1:
                timePeriod = TimePeriod.Afternoon;
                break;
            case 2:
                timePeriod = TimePeriod.Evening;
                break;

            default: 
                timePeriod = TimePeriod.Morning;
                break;
        }

        Debug.Log("Current Time Period " + timePeriod.ToString());
        Events.OnChangeTimePeriod.Invoke();
    }

    public void OnSceneChange()
    {
        //Save data 
        PlayerData playerData = SingletonManager.Get<PlayerData>();
        if (playerData)
        {
            playerData.savedTimeIndex = timeIndex;
            playerData.savedTimePeriod = timePeriod;
        }
    }
}
