using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ImHungryManager : MinigameManager
{
    [Header("Setup")]
    public SpawnManager SpawnManager;
    public int NumOfIngredients;
    public List<GameObject> IngredientsToSpawn;
    public GameObject Pot;

    private Pot pot;
    private PlayerProgress playerProgress; 

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();   
    }

    public override void Initialize()
    {
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.GetComponent<SceneChange>();
        Assert.IsNotNull(Pot, "Pot is null or is not set");
        SpawnManager = SingletonManager.Get<SpawnManager>();
        playerProgress = SingletonManager.Get<PlayerProgress>();
        //SpawnManager.ObjectToSpawn = IngredientsToSpawn;
        pot = Pot.GetComponent<Pot>();
        Events.OnObjectiveComplete.AddListener(CheckIfFinished);
    }



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
        SingletonManager.Get<UIManager>().ActivateGameUI();

        //Spawn Ingredients 
        SpawnManager.SpawnRandomObjectsInStaticPositions();

        //Count progress in Player Progress 
        if (playerProgress)
        {
            playerProgress.imHungryTracker.numOfAttempts += 1;
        }
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

        //Deactivate Pot UI
        if (pot)
        {
            pot.DeactivateTempChoice();
        }

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


    #endregion

    #region Minigame Checker Functions

    public override void CheckIfFinished()
    {
        if (pot == null) { return; }
        if (pot.IsCooked)
        {
            OnWin();
        }
    }

    public override void OnWin()
    {
        SingletonManager.Get<MiniGameTimer>().StopCountdownTimer();
        isCompleted = true;
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateGoodResult();
        SingletonManager.Get<PlayerData>().isImHungryFinished = true;
        if (playerProgress)
        {
            playerProgress.imHungryTracker.time = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.imHungryTracker.numOfTimesCompleted += 1;
        }
        Debug.Log("Minigame complete");
    }

    public override void OnMinigameLose()
    {
        SingletonManager.Get<MiniGameTimer>().StopCountdownTimer();
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();
        if (playerProgress)
        {
            playerProgress.imHungryTracker.time = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.imHungryTracker.numOfTimesFailed += 1;
        }
        Debug.Log("Minigame lose");
    }

    public override void OnMinigameFinished()
    {
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }

    #endregion

}
