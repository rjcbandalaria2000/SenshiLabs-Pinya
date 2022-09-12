using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class CleanTheHouseManager : MinigameManager
{
    [Header("Setup Values")]
    public int  NumberOfToys = 1;
    public int  NumberOfDust = 1;
    public bool isCompleted = false;

    [Header("Player Values")]
    public int  NumberOfToysKept = 0;
    public int  NumberOfDustSwept = 0;


    private SpawnManager spawnManager;


    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        spawnManager = SingletonManager.Get<SpawnManager>();
        spawnManager.NumToSpawn[0] = NumberOfToys;
        spawnManager.NumToSpawn[1] = NumberOfDust;
        spawnManager.SpawnNoRepeat();
        isCompleted = false;
        Events.OnObjectiveUpdate.Invoke();

    }

    public override void CheckIfFinished()
    {
        if(NumberOfToysKept >= NumberOfToys && NumberOfDustSwept >= NumberOfDust)
        {
            if (!isCompleted)
            {   isCompleted=true;
                Debug.Log("Minigame complete");
                //Events.OnSceneLoad.Invoke();
                Events.OnSceneChange.Invoke();
                
                Assert.IsNotNull(sceneChange, "Scene change is null or not set");
                sceneChange.OnChangeScene(NameOfNextScene);
            }
            
        }
        //else
        //{
        //    Debug.Log("Minigame Fail");
        //    Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        //    sceneChange.OnChangeScene(NameOfNextScene);
        //}
    }

    public void AddTrashThrown(int count)
    {
        NumberOfToysKept += count;
        Events.OnObjectiveUpdate.Invoke();
        CheckIfFinished();
    }

    public void AddDustSwept(int count)
    {
        NumberOfDustSwept += count;
        Events.OnObjectiveUpdate.Invoke();
        CheckIfFinished();
    }

    public int GetRemainingDust()
    {
        return NumberOfDust - NumberOfDustSwept;
    }

    public int GetRemainingToys()
    {
        return NumberOfToys - NumberOfToysKept;
    }
    

}
