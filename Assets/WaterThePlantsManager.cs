using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterThePlantsManager : MinigameManager
{
    public List<GameObject> Plants;
     
    // Start is called before the first frame update
    void Start()
    {
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void CheckIfFinished()
    {
        if (AreAllPlantsWatered())
        {
            Debug.Log("Finished Watering the Plants");
        }
    }

    public override void OnWin()
    {
        base.OnWin();
    }

    public override void OnLose()
    {
        base.OnLose();
    }

    public bool AreAllPlantsWatered()
    {
        if(Plants == null) { return false; }
        bool AllWatered = false;
        foreach(GameObject plant in Plants)
        {
            Plant plantObj = plant .GetComponent<Plant>();

            if (plantObj)
            {
                //Check all the plants if all is watered
                Debug.Log(plantObj.gameObject.name+" is watered? " + plantObj.IsWatered);
                AllWatered = plantObj.IsWatered;
                if (!AllWatered)
                {
                    //if at least one plant is not watered break the loop and return false
                    break;
                }
            }
            
        }
        return AllWatered;

    }


}
