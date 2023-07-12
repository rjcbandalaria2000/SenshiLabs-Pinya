using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum HidingSpot
{
    Logs,
    Leaves
}

public class HideSeekManager : MinigameManager
{
    public GameObject               children;
    public int                      score;
    public List<GameObject>         spawnPoints;
    
    public int                      childCount;
    public int                      objectiveScore;
    Coroutine                       spawnRoutine;

    [Header("Countdown Timer")]
    public float                    GameStartTime = 3f;
    public DisplayGameCountdown     CountdownTimerUI;

    [Header("Motivational Meter")]
    public float                    earnedMotivationalValue = 15f;

    private int                     RNG;
    private PlayerProgress          playerProgress;

    public GameObject logs, leaves;

    public List<SpawnHidingInfo> hidingInfo = new List<SpawnHidingInfo>();
    private void Start()
    {
        childCount = 0;
        sceneChange = this.GetComponent<SceneChange>();
        Initialize();
    }

    private void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
        Events.OnSceneChange.RemoveListener(OnSceneChange);

    }
    public override void Initialize()
    {
        id = Constants.HIDE_AND_SEEK_NAME;
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        playerProgress = SingletonManager.Get<PlayerProgress>();
        playerData = SingletonManager.Get<PlayerData>();
        earnedMotivationalValue = 20f;
        SingletonManager.Get<UIManager>().ActivateMiniGameMainMenu();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        Events.OnSceneChange.AddListener(OnSceneChange);
        startMinigameRoutine = null;

    }

    public void RandomizeHidingSpot()
    {
        
        float rand = Random.Range(0.0f, 1f);

        SpawnHidingInfo cacheInfo = new SpawnHidingInfo();

        if (rand < 0.5f)
        {
            cacheInfo.HidingObject = logs;
            cacheInfo.HidingSpot = HidingSpot.Logs;
        }
        else
        {
            cacheInfo.HidingObject = leaves;
            cacheInfo.HidingSpot = HidingSpot.Leaves;
        }

        hidingInfo.Add(cacheInfo);
    }

    public void SpawnHidingSpots()
    {
        for(int i = 0; i< spawnPoints.Count; i++)
        {
            RandomizeHidingSpot();
            GameObject hideObj = Instantiate(hidingInfo[i].HidingObject, spawnPoints[i].transform.position, Quaternion.identity);

        }

    }

    IEnumerator spawn()
    {
        List<GameObject> list = new List<GameObject>();

        SpawnHidingSpots();

        for (int i = 0; i < spawnPoints.Count; i++)
        {

           // RandomizeHidingSpot();

            list = spawnPoints;
            int randomPoint = Random.Range(0, list.Count);

            GameObject child = Instantiate(children, list[randomPoint].transform.position, Quaternion.identity);

            childCount += 1;
            list.RemoveAt(randomPoint);

        }
        objectiveScore = childCount;
        yield return null;

    }
    public override void CheckIfFinished()
    {
        if (score >= objectiveScore)
        {
            OnWin();
        }
        else if(SingletonManager.Get<MiniGameTimer>().GetTimer() <= 0)
        {
           OnMinigameLose();
        }

    }

    public override void OnWin()
    {
        Debug.Log("Minigame complete");
        IncreaseMotivationMeter(earnedMotivationalValue);
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateGoodResult();
        SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
       

        if (playerProgress)
        {
            playerProgress.hideSeekTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.hideSeekTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
            playerProgress.hideSeekTracker.numOfTimesCompleted += 1;
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
            playerProgress.hideSeekTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.hideSeekTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
            playerProgress.hideSeekTracker.numOfTimesFailed += 1;
        }
    }


    public override void StartMinigame()
    {

        startMinigameRoutine = StartCoroutine(StartMinigameCounter());

    }

    protected override IEnumerator StartMinigameCounter()
    {
        gameStartTimer = GameStartTime;

        //Deactivate Minigame Main Menu
        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        Cursor.visible = false;
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
        //Wait till the game countdown is finish
        while (gameStartTimer > 0)
        {
            gameStartTimer -= 1 * Time.deltaTime;
            countdownTimerUI.UpdateCountdownSprites((int)gameStartTimer);
            yield return null;
        }
        //After Game Countdown
        //Activate GameUI and Timer

        Cursor.visible = true;
        spawnRoutine = StartCoroutine(spawn());

        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<UIManager>().ActivateGameUI();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();

        SingletonManager.Get<DisplayChildCount>().updateChildCount();

        Debug.Log("Refresh Score board");
        //Spawn objects

        isCompleted = false;
    }

    public void continueScene()
    {
        Debug.Log("Minigame complete");
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }

    public void gameOver()
    {
        Debug.Log("Minigame lose");
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

    public void IncreaseMotivationMeter(float motivationValue)
    {
        Assert.IsNotNull(playerData, "Player data is not set or is null");
        if (playerData.storedMotivationData < playerData.maxMotivationData)
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
    public bool IsMinigameInTaskList()
    {
        playerData = SingletonManager.Get<PlayerData>();
        if (playerData)
        {
            if (playerData.requiredTasks.Count <= 0) { return false; }
            foreach (string minigameID in playerData.requiredTasks)
            {
                if (minigameID == id)
                {
                    Debug.Log("Current minigame is in the list");
                    return true;
                }
            }
        }
        return false;

    }
}
