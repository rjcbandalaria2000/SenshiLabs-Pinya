using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [Header("States")]
    public bool IsWatering;

    [Header("Values")]
    public float WaterValue;
    public float WaterSpeed;

    public GameObject PlantToWater;

    private Plant plantTarget;
    private Coroutine waterCanControlsRoutine;
    private Coroutine wateringPlantRoutine;

    // Start is called before the first frame update
    void Start()
    {
        waterCanControlsRoutine = null;
        waterCanControlsRoutine = StartCoroutine(OnClickControls());
    }

    IEnumerator OnClickControls()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(Input.GetMouseButtonDown(0).ToString() + " is pushed");
                if (!IsWatering)
                {
                    IsWatering = true;
                    if (PlantToWater) 
                    { 
                        StartWateringPlant(); 
                    }
                    
                }
                
            }
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log(Input.GetMouseButtonDown(0).ToString() + " is released");
                if (IsWatering)
                {
                    IsWatering = false;
                    StopWateringPlant();
                }
            }
            yield return null;
        }
        
    }

    public void StartWateringPlant()
    {
        wateringPlantRoutine = StartCoroutine(WateringPlant());
    }

    public void StopWateringPlant()
    {
        if(wateringPlantRoutine != null)
        {
            StopCoroutine(wateringPlantRoutine);
        }
    }

    IEnumerator WateringPlant()
    {
        plantTarget = PlantToWater.GetComponent<Plant>();
        if (plantTarget == null)
        {
            yield break;
        }
        while (IsWatering)
        {
            yield return new WaitForSeconds(1 / WaterSpeed);
            plantTarget.AddWater(WaterValue);
            yield return null;  
        }
        
    }

    private void OnMouseDown()
    {
        IsWatering = true;
    }

    private void OnMouseUp()
    {
        IsWatering=false;
    }
}
