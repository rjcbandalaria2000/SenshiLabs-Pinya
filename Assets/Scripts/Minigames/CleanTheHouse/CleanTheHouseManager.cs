using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class CleanTheHouseManager : MinigameManager
{
    [Header("Setup Values")]
    public int                  numberOfToys = 1;
    public int                  numberOfDust = 1;
    public List<GameObject>     objectsToSpawn = new();
   
    [Header("Player Score")]
    public int                  numberOfToysKept = 0;
    public int                  numberOfDustSwept = 0;

    private SpawnManager        spawnManager;
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        Initialize();
    }
    public override void Initialize()
    { 
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        spawnManager = SingletonManager.Get<SpawnManager>();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        startMinigameRoutine = null;
        spawnManager.NumToSpawn[0] = numberOfToys;
        spawnManager.NumToSpawn[1] = numberOfDust;
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

    #region Getter Functions
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

    #endregion

    #region Starting Minigame Functions
    public override void StartMinigame()
    {
        gameStartTimer = gameStartTime;
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());
    }

    protected override IEnumerator StartMinigameCounter()
    {
        //Deactivate Minigame Main Menu
        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        //Start Curtain Transition
        SingletonManager.Get<TransitionManager>().ChangeAnimation(TransitionManager.CURTAIN_OPEN);

        //Wait for the animation to finish 
        if (transitionManager != null)
        {
            while (!transitionManager.IsAnimationFinished())
            {
                yield return null;
            }
        }
        //Activate Game Countdown
        SingletonManager.Get<UIManager>().ActivateGameCountdown();
        countdownTimerUI.UpdateCountdownSprites((int)gameStartTimer);
        //Wait till the game countdown is finish
        while (gameStartTimer > 0)
        {
            gameStartTimer -= 1 * Time.deltaTime;
            countdownTimerUI.UpdateCountdownSprites((int)gameStartTimer);
            yield return null;
        }
        //After Game Countdown
        //Activate GameUI and Timer
        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();
        Events.OnObjectiveUpdate.Invoke();
        Debug.Log("Refresh Score board");
        //Spawn objects
        spawnManager.SpawnRandomNoRepeat();
        isCompleted = false;

    }

    #endregion

    #region Exit Minigame Functions
    public override void OnExitMinigame()
    {
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }

    protected override IEnumerator ExitMinigame()
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
    public override void OnMinigameFinished()
    {
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }

    #endregion

    #region Minigame Checkers

    public override void CheckIfFinished()
    {
        if (numberOfToysKept >= numberOfToys && numberOfDustSwept >= numberOfDust)
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

    #endregion
}
