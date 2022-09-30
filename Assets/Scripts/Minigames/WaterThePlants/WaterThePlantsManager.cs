using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaterThePlantsManager : MinigameManager
{
    public List<GameObject> Plants;
     
    // Start is called before the first frame update
    void Start()
    {
        sceneChange = this.GetComponent<SceneChange>();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
    }

    private void Update()
    {
        //CheckIfFinished();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void CheckIfFinished()
    {
        if (AreAllPlantsWatered())
        {
            Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
            Debug.Log("Finished Watering the Plants");
            OnMinigameFinished();
        }
        else if (SingletonManager.Get<MiniGameTimer>().getTimer() <= 0)
        {
            Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
            Debug.Log("Fail Watering the Plants");
            OnMinigameFinished();
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

    public override void OnMinigameFinished()
    {
        if(NameOfNextScene == null) { return; }
        Assert.IsNotNull(sceneChange, "Scene change is null or is not set");
        sceneChange.OnChangeScene(NameOfNextScene);
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
