using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using TMPro;

public class CleanTheHouseManager : MinigameManager
{
    [Header("Setup Values")]
    public int                  numberOfToys = 1;
    public int                  numberOfDust = 1;
   
    [Header("Player Score")]
    public int                  numberOfToysKept = 0;
    public int                  numberOfDustSwept = 0;

    [Header("Countdown Timer")]
    public float                gameStartTime = 3f;
    public DisplayGameCountdown countdownTimerUI;

    private float               gameStartTimer = 0;
    private SpawnManager        spawnManager;
    private Coroutine           startMinigameRoutine;
    private TransitionManager   transitionManager;
    private Coroutine           exitMinigameRoutine;

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
        if(numberOfToysKept >= numberOfToys && numberOfDustSwept >= numberOfDust)
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
        numberOfToysKept += count;
        Events.OnObjectiveUpdate.Invoke();
        CheckIfFinished();
    }

    public void AddDustSwept(int count)
    {
        numberOfDustSwept += count;
        Events.OnObjectiveUpdate.Invoke();
        CheckIfFinished();
    }

    public int GetRemainingDust()
    {
        return numberOfDust - numberOfDustSwept;
    }

    public int GetRemainingToys()
    {
        return numberOfToys - numberOfToysKept;
    }

    public float GetTimeElapsed()
    {
        return maxTimer - timer;
    }

    public override void Initialize()
    {
        //SingletonManager.Get<UIManager>().ActivateMiniGameMainMenu();
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        spawnManager = SingletonManager.Get<SpawnManager>();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        startMinigameRoutine = null;
        spawnManager.NumToSpawn[0] = numberOfToys;
        spawnManager.NumToSpawn[1] = numberOfDust;
    }

    public override void StartMinigame()
    {
        gameStartTimer = gameStartTime;
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
        SingletonManager.Get<TransitionManager>().ChangeAnimation(TransitionManager.CURTAIN_OPEN);

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
        countdownTimerUI.UpdateCountdownTimer(gameStartTimer);
        //Wait till the game countdown is finish
        while(gameStartTimer > 0)
        {
            gameStartTimer -= 1 * Time.deltaTime;
            countdownTimerUI.UpdateCountdownTimer(gameStartTimer);
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
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
        //Events.OnSceneChange.Invoke();
        //Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        //sceneChange.OnChangeScene(NameOfNextScene);
    }

    IEnumerator ExitMinigame()
    {
        // Play close animation
        transitionManager.ChangeAnimation(TransitionManager.CURTAIN_CLOSE);
        //Deactivate active UI 
        SingletonManager.Get<UIManager>().DeactivateResultScreen();
        SingletonManager.Get<UIManager>().DeactivateTimerUI();
        SingletonManager.Get<UIManager>().DeactivateGameUI();
        //Wait for transition to end
        while (!transitionManager.IsAnimationFinished())
        {
            Debug.Log("Transition to closing");
            yield return null;
        }
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
        yield return null;
    }
}
