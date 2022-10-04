using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SleepingMinigameManager : MinigameManager
{
    [Header("Setup Values")]
    public int RequiredPoints;
    public int PlayerPoints;

    [Header("MinigameObject")]
    public GameObject basket;
    public GameObject spawner;
    public GameObject playerCanvas;

    [Header("Countdown Timer")]
    public float GameStartTime = 3f;
    public DisplayGameCountdown CountdownTimerUI;

    private SpawnManager spawnManager;


    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        basket.SetActive(false);
        spawner.SetActive(false);
        playerCanvas.SetActive(false);
        transitionManager = SingletonManager.Get<TransitionManager>();
        spawnManager = SingletonManager.Get<SpawnManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();

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
  
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());

        
        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();

  
       

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
            spawnManager.StopTimedUnlimitedSpawnBox();
            Debug.Log("Minigame Complete");
            basket.SetActive(false);
            //if(sceneChange == null) { return; }
            //if(NameOfNextScene == null) { return; }
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateGoodResult();
            SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;

            //SingletonManager.Remove<SleepingMinigameManager>();
            //SingletonManager.Remove<SpawnManager>();

        }
        else if(PlayerPoints <= RequiredPoints && SingletonManager.Get<MiniGameTimer>().GetTimer() <= 0)
        {
            spawnManager.StopTimedUnlimitedSpawnBox();
            Debug.Log("Minigame Fail");
            basket.SetActive(false);
            //if (sceneChange == null) { return; }
            //if (NameOfNextScene == null) { return; }
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateBadResult();
            SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;

            //SingletonManager.Remove<SleepingMinigameManager>();
            //SingletonManager.Remove<SpawnManager>();

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
        spawnManager.StartUnlimitedTimedSpawnBoxSpawn();
        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();

       

        Events.OnObjectiveUpdate.Invoke();
        Debug.Log("Refresh Score board");
        //Spawn objects

        isCompleted = false;
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


}
