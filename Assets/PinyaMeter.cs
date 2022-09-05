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

    [Header("Events")]
    public UnityEvent   EvtChangeValue = new();

    // Start is called before the first frame update
    void Start()
    {
        IntitializePinyaMeter();
    }

    public void IntitializePinyaMeter()
    {
        PinyaValue = MaxPinyaValue;
        EvtChangeValue.Invoke();
    }

    public void IncreasePinyaMeter(float value)
    {
        if (PinyaValue < MaxPinyaValue)
        {
            PinyaValue += value;
        }
        else
        {
            Debug.Log("Pinya Value is max");
        }
    }

    public void DecreasePinyaMeter(float value)
    {
        if(PinyaValue > 0)
        {
            PinyaValue -= value;
        }
        else
        {
            Debug.Log("Pinya value is already 0");
        }
    }

}
