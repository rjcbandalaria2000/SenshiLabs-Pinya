using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class PinyaMeter : MonoBehaviour
{
    [Header("Values")]
    public float        pinyaValue;
    public float        maxPinyaValue;

    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.Get<PlayerData>().hasSaved)
        {
            pinyaValue = SingletonManager.Get<PlayerData>().storedPinyaData;
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
        pinyaValue = maxPinyaValue;
        //Events.OnChangeMeter.Invoke();
    }
    public void IncreasePinyaMeter(float value)
    {
        //If the current value is less than the max value 
        if (pinyaValue < maxPinyaValue)
        {
            pinyaValue += value;
        }
        else
        {
            Debug.Log("Pinya Value is max");
        }
        Events.OnChangeMeter.Invoke();
    }

    public void DecreasePinyaMeter(float value)
    {
        if(pinyaValue > 0)
        {
            pinyaValue -= value;
        }
        if (pinyaValue <=0)
        {
            // When Pinya meter is 0
            Events.OnPinyaEmpty.Invoke();
            Debug.Log("Pinya value is already 0");
        }
        Events.OnChangeMeter.Invoke();
    }

    #endregion
}
