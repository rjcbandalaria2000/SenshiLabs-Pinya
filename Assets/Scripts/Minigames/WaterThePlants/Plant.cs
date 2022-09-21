using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("WaterBar")]
    public Image waterBar;

    // Start is called before the first frame update
    void Start()
    {
        IsWatered = false;
        waterBar.fillAmount = 0;
        ChangeModel();
    }

    public void AddWater(float waterValue)
    {
        if(CurrentWater < MaxWater)
        {
            CurrentWater += waterValue;
            waterBar.fillAmount = CurrentWater / MaxWater;
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
