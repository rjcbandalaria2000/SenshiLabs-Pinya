using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Plant : MonoBehaviour
{
    [Header("Values")]
    public float        CurrentWater;
    public float        MaxWater;

    [Header("Animation")]
    public float        animationTime = 1f;

    [Header("States")]
    public bool         IsWatered =false;

    [Header("Models")]
    public GameObject   DehydratedModel;
    public GameObject   MiddleModel;
    public GameObject   HydratedModel;

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
            waterBar.DOFillAmount(CurrentWater / MaxWater, animationTime);
            //waterBar.fillAmount = CurrentWater / MaxWater;
        }
        else
        {
            if (!IsWatered)
            {
                IsWatered = true;
                SingletonManager.Get<WaterThePlantsManager>().plantsWatered++;
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
        if (MiddleModel == null) { return; }

        if (IsWatered || CurrentWater >= MaxWater) // fully watered 
        {
            HydratedModel.SetActive(true);
            MiddleModel.SetActive(false);
            DehydratedModel.SetActive(false);
        }
        else if (CurrentWater >= MaxWater/2 && CurrentWater > 0)// middle 
        {
            HydratedModel.SetActive(false);
            MiddleModel.SetActive(true);
            DehydratedModel.SetActive(false);
        } 
        else// not yet watered
        {
            HydratedModel.SetActive(false);
            DehydratedModel.SetActive(true);
            MiddleModel.SetActive(false) ;
        }

    }

    
}
