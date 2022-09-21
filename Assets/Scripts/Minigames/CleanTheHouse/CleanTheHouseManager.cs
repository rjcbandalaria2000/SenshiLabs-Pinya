using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using TMPro;

public class CleanTheHouseManager : MinigameManager
{
    [Header("Setup Values")]
    public int  NumberOfToys = 1;
    public int  NumberOfDust = 1;
   
    [Header("Player Values")]
    public int  NumberOfToysKept = 0;
    public int  NumberOfDustSwept = 0;

    [Header("Countdown Timer")]
    public float GameStartTime = 3f;
    public DisplayGameCountdown CountdownTimerUI;

    private float GameStartTimer = 0;
    private SpawnManager spawnManager;
    private Coroutine startMinigameRoutine;
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        Initialize();
    }

    public override void CheckIfFinished()
    {
        if(NumberOfToysKept >= NumberOfToys && NumberOfDustSwept >= NumberOfDust)
        {
            OnWin();
            //if (!isCompleted)
            //{   isCompleted=true;
            //    SingletonManager.Get<PlayerData>().IsCleanTheHouseFinished = true;
            //    Debug.Log("Minigame complete");
            //    //Events.OnSceneLoad.Invoke();
            //    //Events.OnSceneChange.Invoke();
                
            //    //Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            //    //sceneChange.OnChangeScene(NameOfNextScene);
            //}   
        }
    }

    public override void OnMinigameLose()
    {
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();
        Debug.Log("Minigame lose");
        //Events.OnSceneChange.Invoke();
        //Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        //sceneChange.OnChangeScene(NameOfNextScene);
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
        GameStartTimer = GameStartTime;
        SingletonManager.Get<UIManager>().ActivateGameCountdown();
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());
        SingletonManager.Get<UIManager>().deactivateMiniGameMainMenu();
        SingletonManager.Get<UIManager>().activateMiniGameTimer_UI();
    }

    public IEnumerator StartMinigameCounter()
    {
        CountdownTimerUI.UpdateCountdownTimer(GameStartTimer);
        while(GameStartTimer > 0)
        {
            GameStartTimer -= 1 * Time.deltaTime;
            CountdownTimerUI.UpdateCountdownTimer(GameStartTimer);
            yield return null;
        }
        //yield return new WaitForSeconds(GameStartTime);
        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();
       
        spawnManager.SpawnRandomNoRepeat();
        isCompleted = false;
        Events.OnObjectiveUpdate.Invoke();
    }

    public override void OnMinigameFinished()
    {
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }

    public override void OnWin()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateGoodResult();
            SingletonManager.Get<PlayerData>().IsCleanTheHouseFinished = true;
            Debug.Log("Minigame complete");
            //Events.OnSceneLoad.Invoke();
            //Events.OnSceneChange.Invoke();

            //Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            //sceneChange.OnChangeScene(NameOfNextScene);
        }
    }

    public override void OnExitMinigame()
    {
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }
}
