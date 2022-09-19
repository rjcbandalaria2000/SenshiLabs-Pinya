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
    public float time;
    public float endTime;
    public TimePeriod timePeriod;

    [SerializeField] bool isMorning;

    private void Awake()
    {
        SingletonManager.Register(this);
        
    }
    private void Start()
    {
       ChangeTimePeriod();
    }

    public void IncreaseTime(float timeAdd) // initial (change to events)
    {
        if(endTime > time)
        {
            time += timeAdd;
            Events.OnDisplayCycleTime.Invoke();
        }
        else
        {
            StartCoroutine(SingletonManager.Get<GameManager>().dayEnd());
            Debug.Log("DayEnd");
        }
      
    }

    public void ChangeTimePeriod()
    {
        if(SingletonManager.Get<PlayerData>().MinigamesPlayed <= 0)
        {
            timePeriod = TimePeriod.Morning;
        }
        if (SingletonManager.Get<PlayerData>().MinigamesPlayed >= 3)
        {
            timePeriod = TimePeriod.Afternoon;
        }
        if(SingletonManager.Get<PlayerData>().MinigamesPlayed >= 6)
        {
            timePeriod = TimePeriod.Evening;
        }

            Debug.Log("Current Time Period " + timePeriod.ToString());
    }
}
