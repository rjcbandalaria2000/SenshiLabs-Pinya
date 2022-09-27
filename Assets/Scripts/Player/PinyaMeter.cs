using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class PinyaMeter : MonoBehaviour
{
    [Header("Values")]
    public float        PinyaValue;
    public float        MaxPinyaValue;

    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.Get<PlayerData>().HasSaved)
        {
            PinyaValue = SingletonManager.Get<PlayerData>().storedPinyaData;
        }
        else
        {
            IntitializePinyaMeter();
        }
        Events.OnChangeMeter.Invoke();
    }

   
    #region Public Functions 
    public void IntitializePinyaMeter()
    {
        PinyaValue = MaxPinyaValue;
        Events.OnChangeMeter.Invoke();
    }
    public void IncreasePinyaMeter(float value)
    {
        //If the current value is less than the max value 
        if (PinyaValue < MaxPinyaValue)
        {
            PinyaValue += value;
        }
        else
        {
            Debug.Log("Pinya Value is max");
        }
        Events.OnChangeMeter.Invoke();
    }

    public void DecreasePinyaMeter(float value)
    {
        if(PinyaValue > 0)
        {
            PinyaValue -= value;
        }
        else
        {
            // When Pinya meter is 0
            Events.OnPinyaEmpty.Invoke();
            Debug.Log("Pinya value is already 0");
        }
        Events.OnChangeMeter.Invoke();
    }

    #endregion
}
