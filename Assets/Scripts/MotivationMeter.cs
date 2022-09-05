using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MotivationMeter : MonoBehaviour
{
    public float    MotivationAmount;
    public float    MaxMotivation;

    [Header("Unity Events")]
    public UnityEvent EvtChangeMeter = new();

    
    // Start is called before the first frame update
    void Start()
    {
        InitializeMeter();
    }

    public void InitializeMeter() {

        MotivationAmount = MaxMotivation;
        EvtChangeMeter.Invoke();
    }

}
