using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [Header("States")]
    public bool         IsWatering;

    [Header("Values")]
    public float        WaterValue;
    public float        WaterSpeed;

    [Header("Models")]
    public GameObject   WateringModel;
    public GameObject   ReadyModel;

    [Header("Plant Target")]
    public GameObject   PlantToWater;

    private Plant       plantTarget;
    private Coroutine   waterCanControlsRoutine;
    private Coroutine   wateringPlantRoutine;
    private MouseFollow mouseFollow;

    // Start is called before the first frame update
    void Start()
    {
        waterCanControlsRoutine = null;
        waterCanControlsRoutine = StartCoroutine(OnClickControls());
        ChangeModel();
    }

    IEnumerator OnClickControls()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsWatering)
                {
                    IsWatering = true;
                    ChangeModel();
                    
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
                    plantTarget = null;
                    ChangeModel();
                   
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
       
        while (IsWatering)
        { 
            plantTarget = PlantToWater.GetComponent<Plant>();
            if (plantTarget == null){ yield break; }
            yield return new WaitForSeconds(1 / WaterSpeed);
            plantTarget.AddWater(WaterValue);
            Debug.Log("Watering the plant" + plantTarget.gameObject.name);
            yield return null;  
        }

    }

    public void ChangeModel()
    {
        if (IsWatering)
        {
            ReadyModel.SetActive(false);
            WateringModel.SetActive(true);
        }
        else
        {
            ReadyModel.SetActive(true);
            WateringModel.SetActive(false);
        }
    }
}
