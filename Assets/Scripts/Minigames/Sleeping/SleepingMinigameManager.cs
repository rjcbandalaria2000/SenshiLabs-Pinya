using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingMinigameManager : MinigameManager
{
    [Header("Setup Values")]
    public int RequiredPoints;
    public int PlayerPoints;

    private SpawnManager spawnManager;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = SingletonManager.Get<SpawnManager>();
        spawnManager.StartUnlimitedTimedSpawnBoxSpawn();
        sceneChange = this.GetComponent<SceneChange>();
      //  Events.OnObjectiveUpdate.Invoke();
        Events.UpdateScore.Invoke();
    }

    private void Update()
    {
        if(SingletonManager.Get<MiniGameTimer>().getTimer() <= 0)
        {
            CheckIfFinished();
        }
    }

    public override void Initialize()
    {
        
    }

    public override void CheckIfFinished()
    {
        if(PlayerPoints >= RequiredPoints)
        {
            spawnManager.StopTimedUnlimitedSpawnBox();
            Debug.Log("Minigame Complete");
            SingletonManager.Remove<SleepingMinigameManager>();
            SingletonManager.Remove<SpawnManager>();
            if(sceneChange == null) { return; }
            if(NameOfNextScene == null) { return; }
            Events.OnSceneChange.Invoke();
            sceneChange.OnChangeScene(NameOfNextScene);

        }
        else if(PlayerPoints <= RequiredPoints && SingletonManager.Get<MiniGameTimer>().getTimer() <= 0)
        {
            spawnManager.StopTimedUnlimitedSpawnBox();
            Debug.Log("Minigame Fail");
            SingletonManager.Remove<SleepingMinigameManager>();
            SingletonManager.Remove<SpawnManager>();
            if (sceneChange == null) { return; }
            if (NameOfNextScene == null) { return; }
            Events.OnSceneChange.Invoke();
            sceneChange.OnChangeScene(NameOfNextScene);
           
        }
    }

    public void GetPlayerPoints(int points)
    {

    }

    public override void OnMinigameFinished()
    {
       
    }

    public override void OnWin()
    {
        
    }

    public override void OnLose()
    {
       
    }

}
