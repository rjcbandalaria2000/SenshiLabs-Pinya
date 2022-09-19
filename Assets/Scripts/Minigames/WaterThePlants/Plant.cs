using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Values")]
    public float    CurrentWater;
    public float    MaxWater;

    [Header("States")]
    public bool     IsWatered =false;

    [Header("Models")]
    public GameObject DehydratedModel;
    public GameObject HydratedModel;

    // Start is called before the first frame update
    void Start()
    {
        IsWatered = false;
        ChangeModel();
    }

    public void AddWater(float waterValue)
    {
        if(CurrentWater < MaxWater)
        {
            CurrentWater += waterValue;
        }
        else
        {
            if (!IsWatered)
            {
                IsWatered = true;
                Events.OnObjectiveUpdate.Invoke();
                Debug.Log("Fully watered");
            }
           
        }
        ChangeModel();
    }

    public void ChangeModel()
    {
        if(DehydratedModel == null) { return; }
        if(HydratedModel == null) { return; }

        if (IsWatered)
        {
            HydratedModel.SetActive(true);
            DehydratedModel.SetActive(false);
        }
        else
        {
            HydratedModel.SetActive(false);
            DehydratedModel.SetActive(true);
        }

    }

    
}
