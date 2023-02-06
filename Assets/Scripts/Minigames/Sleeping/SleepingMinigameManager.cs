using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SleepingMinigameManager : MinigameManager
{
    [Header("Setup Values")]
    public int                  RequiredPoints;
    public int                  PlayerPoints;
    public float                motivationalPoints = 20f;

    [Header("MinigameObject")]
    public GameObject           basket;
    public GameObject           spawner;
    public GameObject           playerCanvas;

    [Header("Countdown Timer")]
    public float                GameStartTime = 3f;
    public DisplayGameCountdown CountdownTimerUI;

    private SpawnManager        spawnManager;
    private PlayerProgress      playerProgress;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        //  Events.OnObjectiveUpdate.Invoke();
        Events.UpdateScore.Invoke();
    }

    private void Update()
    {
        if(SingletonManager.Get<MiniGameTimer>().GetTimer() <= 0)
        {
            CheckIfFinished();
        }
    }

    public override void Initialize()
    {
        basket.SetActive(false);
        spawner.SetActive(false);
        playerCanvas.SetActive(false);
        transitionManager = SingletonManager.Get<TransitionManager>();
        spawnManager = SingletonManager.Get<SpawnManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        motivationalPoints = 20f;
        SingletonManager.Get<UIManager>().ActivateMiniGameMainMenu();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        Events.OnSceneChange.AddListener(OnSceneChange);
        startMinigameRoutine = null;


    }

    private void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        DisableControls();

    }

    public override void StartMinigame()
    {
        gameStartTimer = gameStartTime;
       
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());

    }

    public override void CheckIfFinished()
    {
        if(PlayerPoints >= RequiredPoints)
        {
            OnWin();
        }
        else if(PlayerPoints <= RequiredPoints && SingletonManager.Get<MiniGameTimer>().GetTimer() <= 0)
        {
            OnMinigameLose();
        }
    }

    protected override IEnumerator StartMinigameCounter()
    {
        gameStartTimer = GameStartTime;

        //Deactivate Minigame Main Menu
        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        //Start Curtain Transition
        transitionManager.ChangeAnimation(TransitionManager.CURTAIN_OPEN);

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
        //countdownTimerUI.UpdateCountdownTimer(gameStartTimer);
        //Wait till the game countdown is finish
        while (gameStartTimer > 0)
        {
            gameStartTimer -= 1 * Time.deltaTime;
            countdownTimerUI.UpdateCountdownSprites((int)gameStartTimer);
            yield return null;
        }
        //After Game Countdown
        //Activate GameUI and Timer

        basket.SetActive(true);
        spawner.SetActive(true);
        playerCanvas.SetActive(true);

        //spawnManager.StartUnlimitedTimedSpawnBoxSpawn();
        spawnManager.spawnChance();

        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();

       

        Events.OnObjectiveUpdate.Invoke();
        Debug.Log("Refresh Score board");
        //Spawn objects

        isCompleted = false;

        //Count the attempt in Player progress
        if (playerProgress)
        {
            playerProgress.sleepTracker.numOfAttempts += 1;
        }
    }

    public void continueButton()
    {
        Debug.Log("Minigame complete");
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }

    public void exitButton()
    {
        Debug.Log("Minigame complete");
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }

    protected override IEnumerator ExitMinigame()
    {
        // Play close animation
        if(transitionManager)
        {
            transitionManager.ChangeAnimation(TransitionManager.CURTAIN_CLOSE);
        }
        else
        {
            Debug.Log("transition null");
        }
       
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

    public override void OnWin()
    {
        // spawnManager.StopTimedUnlimitedSpawnBox();
        spawnManager.stopSpawnChance();
        Debug.Log("Minigame Complete");
        basket.SetActive(false);
        //if(sceneChange == null) { return; }
        //if(NameOfNextScene == null) { return; }
        SingletonManager.Get<PlayerData>().isSleepFinished = true;
        IncreaseMotivationalMeter(motivationalPoints);
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateGoodResult();
        SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;

        if (playerProgress)
        {
            playerProgress.sleepTracker.numOfTimesCompleted += 1;
            playerProgress.sleepTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.sleepTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
        }
        //SingletonManager.Remove<SleepingMinigameManager>();
        //SingletonManager.Remove<SpawnManager>();
    }

    public override void OnMinigameLose()
    {
        // spawnManager.StopTimedUnlimitedSpawnBox();
        spawnManager.stopSpawnChance();
        Debug.Log("Minigame Fail");
        basket.SetActive(false);
        //if (sceneChange == null) { return; }
        //if (NameOfNextScene == null) { return; }
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();
        SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;

        if (playerProgress)
        {
            playerProgress.sleepTracker.numOfTimesFailed += 1;
            playerProgress.sleepTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.sleepTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
        }
        //SingletonManager.Remove<SleepingMinigameManager>();
        //SingletonManager.Remove<SpawnManager>();
    }

    public void IncreaseMotivationalMeter(float motivationValue)
    {
        PlayerData playerData = SingletonManager.Get<PlayerData>();
        if(playerData == null) { return; }
        if(playerData.storedMotivationData < playerData.maxMotivationData)
        {
            this.previousVal = SingletonManager.Get<PlayerData>().storedMotivationData;
            SingletonManager.Get<PlayerData>().previousStoredMotivation = this.previousVal;
            playerData.storedMotivationData += motivationValue;
            
        }
       playerData.storedMotivationData = Mathf.Clamp(playerData.storedMotivationData, 0, playerData.maxMotivationData);
    }

    public override void GameMinigamePause()
    {
        Time.timeScale = 0f;
    }

    public override void GameMinigameResume()
    {
        Time.timeScale = 1f;
    }

    public void DisableControls()
    {
        if (basket == null) { return; }

        MouseFollow basketFollow = basket.GetComponent<MouseFollow>();
        if (basketFollow)
        {
            basketFollow.canMove = false;
        }
    }
}
