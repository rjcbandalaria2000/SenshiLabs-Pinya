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
       //ChangeTimePeriod(timeIndex);
    }

    //public void IncreaseTime(float timeAdd) // initial (change to events)
    //{
    //    if(endTime > time)
    //    {
    //        time += timeAdd;
    //        Events.OnDisplayCycleTime.Invoke();
    //    }
    //    else
    //    {
    //        //StartCoroutine(SingletonManager.Get<GameManager>().dayEnd());
    //        Debug.Log("DayEnd");
    //    }
      
    //}

    public void ChangeTimePeriod(int timeIndex)
    {
        //if(SingletonManager.Get<PlayerData>().MinigamesPlayed <= 4)
        //{
        //    timePeriod = TimePeriod.Morning;
        //}
        //if (SingletonManager.Get<PlayerData>().MinigamesPlayed >= 5)
        //{
        //    timePeriod = TimePeriod.Afternoon;
        //}
        //if(SingletonManager.Get<PlayerData>().MinigamesPlayed >= 8)
        //{
        //    timePeriod = TimePeriod.Evening;
        //}
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
}
