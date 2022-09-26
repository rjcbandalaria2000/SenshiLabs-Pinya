using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using TMPro;

public class CleanTheHouseManager : MinigameManager
{
    [Header("Setup Values")]
    public int                  NumberOfToys = 1;
    public int                  NumberOfDust = 1;
   
    [Header("Player Score")]
    public int                  NumberOfToysKept = 0;
    public int                  NumberOfDustSwept = 0;

    [Header("Countdown Timer")]
    public float                GameStartTime = 3f;
    public DisplayGameCountdown CountdownTimerUI;

    private float               GameStartTimer = 0;
    private SpawnManager        spawnManager;
    private Coroutine           startMinigameRoutine;
    private TransitionManager   transitionManager;
    
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
        }
    }

    public override void OnMinigameLose()
    {
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();
        Debug.Log("Minigame lose");
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
        //SingletonManager.Get<UIManager>().ActivateMiniGameMainMenu();
        transitionManager = SingletonManager.Get<TransitionManager>();
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
        //SingletonManager.Get<UIManager>().ActivateGameCountdown();
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());
        //SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        //SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
    }

    public IEnumerator StartMinigameCounter()
    {
        //Deactivate Minigame Main Menu
        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        //Start Curtain Transition
        SingletonManager.Get<TransitionManager>().StartOpeningTransition();

        //Wait for the animation to finish 
        if(transitionManager != null)
        {
            while (!transitionManager.IsAnimationFinished())
            {
                yield return null;
            }
        }
        //Activate Game Countdown
        SingletonManager.Get<UIManager>().ActivateGameCountdown();
        CountdownTimerUI.UpdateCountdownTimer(GameStartTimer);
        while(GameStartTimer > 0)
        {
            GameStartTimer -= 1 * Time.deltaTime;
            CountdownTimerUI.UpdateCountdownTimer(GameStartTimer);
            yield return null;
        }
        //After Game Countdown
        //Activate GameUI and Timer
        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        //Events.OnObjectiveUpdate.Invoke();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();
        Events.OnObjectiveUpdate.Invoke();
        Debug.Log("Refresh Score board");
        spawnManager.SpawnRandomNoRepeat();
        isCompleted = false;
        
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
            SingletonManager.Get<MiniGameTimer>().StopCountdownTimer();
            isCompleted = true;
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateGoodResult();
            SingletonManager.Get<PlayerData>().IsCleanTheHouseFinished = true;
            Debug.Log("Minigame complete");
        }
    }

    public override void OnExitMinigame()
    {
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }
}
