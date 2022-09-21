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
    public float GameStartTime = 3f;

    [Header("Player Values")]
    public int  NumberOfToysKept = 0;
    public int  NumberOfDustSwept = 0;


    private SpawnManager spawnManager;
    private Coroutine startMinigameRoutine;
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        //Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        //sceneChange = this.gameObject.GetComponent<SceneChange>();
        //spawnManager = SingletonManager.Get<SpawnManager>();
        //spawnManager.NumToSpawn[0] = NumberOfToys;
        //spawnManager.NumToSpawn[1] = NumberOfDust;
        //spawnManager.SpawnRandomNoRepeat();
        //isCompleted = false;
        //Events.OnObjectiveUpdate.Invoke();

        Initialize();
        //StartMinigame();

    }

    public override void CheckIfFinished()
    {
        if(NumberOfToysKept >= NumberOfToys && NumberOfDustSwept >= NumberOfDust)
        {
            if (!isCompleted)
            {   isCompleted=true;
                SingletonManager.Get<PlayerData>().IsCleanTheHouseFinished = true;
                Debug.Log("Minigame complete");
                //Events.OnSceneLoad.Invoke();
                Events.OnSceneChange.Invoke();
                
                Assert.IsNotNull(sceneChange, "Scene change is null or not set");
                sceneChange.OnChangeScene(NameOfNextScene);
            }   
        }
    }

    public override void OnMinigameLose()
    {
        Debug.Log("Minigame lose");
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
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

    public override void Initialize()
    {
        SingletonManager.Get<UIManager>().activateMiniGameMainMenu();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        spawnManager = SingletonManager.Get<SpawnManager>();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        startMinigameRoutine = null;
        spawnManager.NumToSpawn[0] = NumberOfToys;
        spawnManager.NumToSpawn[1] = NumberOfDust;
    }

    public override void StartMinigame()
    {
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());
        SingletonManager.Get<UIManager>().deactivateMiniGameMainMenu(); 
        SingletonManager.Get<UIManager>().activateMiniGameTimer_UI();
        //spawnManager.SpawnRandomNoRepeat();
        //isCompleted = false;
        //Events.OnObjectiveUpdate.Invoke();
    }

    public IEnumerator StartMinigameCounter()
    {
        
        yield return new WaitForSeconds(GameStartTime);
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();
       
        spawnManager.SpawnRandomNoRepeat();
        isCompleted = false;
        Events.OnObjectiveUpdate.Invoke();
    }

}
