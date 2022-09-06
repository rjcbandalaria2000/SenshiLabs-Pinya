using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float time;
    public float endTime;

    [SerializeField] bool isMorning;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    
    public void increaseTime(float timeAdd) // initial (change to events)
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
}
