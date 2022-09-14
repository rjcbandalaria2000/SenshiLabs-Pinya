using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Values")]
    public float    CurrentWater;
    public float    MaxWater;

    public bool     IsWatered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddWater(float waterValue)
    {
        if(CurrentWater < MaxWater)
        {
            CurrentWater += waterValue;
        }
        else
        {
            Debug.Log("Fully watered");
        }
    }

    
}
