using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoldingMinigameManager : MinigameManager
{
    public Clothes              ClothesComponent;
    public GameObject           spawnClothes;
    public int                  clothesNum;

    [Header("Countdown Timer")]
    public float GameStartTime = 3f;
    public DisplayGameCountdown CountdownTimerUI;

    [Header("Position")]
    public GameObject           startPos;
    public GameObject           middlePos;
    public GameObject           endPos;

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

    // Update is called once per frame
    void Update()
    {
        if (SingletonManager.Get<MiniGameTimer>().GetTimer() <= 0)
        {
            CheckIfFinished();
        }
    }

    public override void Initialize()
    {
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        playerProgress = SingletonManager.Get<PlayerProgress>();

        spawnClothes.GetComponent<Clothes>().startPos = startPos;
        spawnClothes.GetComponent<Clothes>().middlePos = middlePos;
        spawnClothes.GetComponent<Clothes>().endPos = endPos;
        
        SingletonManager.Get<UIManager>().ActivateMiniGameMainMenu();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        Events.OnSceneChange.AddListener(OnSceneChange);
        startMinigameRoutine = null;

    }

    private void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
        Events.OnSceneChange.RemoveListener(OnSceneChange);

    }

    public override void GameMinigamePause()
    {
        Time.timeScale = 0f;
    }

    public override void GameMinigameResume()
    {
        Time.timeScale = 1f;
    }


    #region Starting Minigame Functions

    public override void StartMinigame()
    {
  
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());

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
    //    countdownTimerUI.UpdateCountdownSprites((int)gameStartTimer);
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

        startMinigameRoutine = StartCoroutine(initializeMiniGame());

        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<UIManager>().ActivateGameUI();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();

        //Count the attempt in player progress 
        if (playerProgress)
        {
            playerProgress.foldTheClothesTracker.totalTime = maxTimer;
            playerProgress.foldTheClothesTracker.numOfAttempts += 1; 
        }
 
        Debug.Log("Refresh Score board");
        //Spawn objects

        isCompleted = false;

        

    }


    IEnumerator initializeMiniGame()
    {
        yield return null;

        GameObject newClothes = Instantiate(spawnClothes, startPos.transform.position, Quaternion.identity);

        if (ClothesComponent == null)
        {

            ClothesComponent = GameObject.FindObjectOfType<Clothes>().GetComponent<Clothes>();


        }
        SingletonManager.Get<DisplayFoldCount>().UpdateFoldCount();

        //  ClothesComponent.gameObject.SetActive(true);

        startMinigameRoutine = null;

    }

    #endregion

    #region Minigame Checkers
    public override void CheckIfFinished()
    {
        if (ClothesComponent.clothes <= 0)
        {
            OnWin();
        }
        else
        {
            OnMinigameLose();
        }

    }

    public override void OnWin()
    {
        Debug.Log("Minigame complete");
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateGoodResult();
        SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
        SingletonManager.Get<PlayerData>().isFoldingClothesFinished = true;
        if (playerProgress)
        {
            playerProgress.foldTheClothesTracker.numOfTimesCompleted += 1;
            playerProgress.foldTheClothesTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.foldTheClothesTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
        }
    }

    public override void OnMinigameLose()
    {
        Debug.Log("Minigame lose");
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();
        SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
        if (playerProgress)
        {
            playerProgress.foldTheClothesTracker.numOfTimesFailed += 1;
            playerProgress.foldTheClothesTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.foldTheClothesTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
        }
    }
    #endregion

    #region Exit Minigame Functions 

    public void gameOver()
    {
        Debug.Log("Minigame lose");
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }
    public void continueScene()
    {
        Debug.Log("Minigame complete");
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }
    protected override IEnumerator ExitMinigame()
    {
        // Play close animation
        if (transitionManager)
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
    #endregion


}
