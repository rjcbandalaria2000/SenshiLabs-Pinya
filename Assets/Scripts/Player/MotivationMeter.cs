using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MotivationMeter : MonoBehaviour
{
    [Header("Values")]
    public float        MotivationAmount;
    public float        MaxMotivation;

    [Header("Threshold Values")]
    public float        minMotivationAmount = 30f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.Get<PlayerData>().hasSaved)
        {
            MotivationAmount = SingletonManager.Get<PlayerData>().storedMotivationData;
        }
        else
        {
            InitializeMeter();
        }
        CheckMotivationalMeter();
        Events.OnChangeMeter.Invoke();
    }

    public void InitializeMeter() {

        MotivationAmount = MaxMotivation;
        //Events.OnChangeMeter.Invoke();
        //EvtChangeMeter.Invoke();
    }

    public void IncreaseMotivation(float value)
    {
        if(MotivationAmount < MaxMotivation)
        {
            MotivationAmount += value;
        }
        else
        {
            Debug.Log("Motivation is maxed out");
        }
        MotivationAmount = Mathf.Clamp(MotivationAmount, 0, MaxMotivation);
        Events.OnChangeMeter.Invoke();
        //EvtChangeMeter.Invoke();
    }

    public void DecreaseMotivation(float value)
    {
        if(MotivationAmount > 0)
        {
            MotivationAmount -= value;  
        }
        else
        {
            Debug.Log("Motivation is already at 0");
        }
        Events.OnChangeMeter.Invoke();
        //EvtChangeMeter.Invoke();
    }

    public void CheckMotivationalMeter()
    {
        if(MotivationAmount <= minMotivationAmount)
        {
            Events.OnEmptyMotivation.Invoke(true);
        }
        else
        {
            Events.OnEmptyMotivation.Invoke(false);
        }
    }

}
