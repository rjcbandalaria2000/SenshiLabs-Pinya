using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GetWaterManager : MinigameManager
{
    [Header("Well")]
    public GameObject   wateringWell;

    [Header("Water UI")]
    public Slider       slider;

    [Header("Setup Values")]
    public int          RequiredNumSwipes = 3;
    public int          NumOfSwipes = 0;

    private PlayerProgress playerProgress;
    private void Awake()
    {
        //Trying this solution since
        //every destroy or load the singleton must be destroyed
        //and replaced with a new singeleton script when it exists
        GetWaterManager getWaterManager = SingletonManager.Get<GetWaterManager>();
        if (getWaterManager != null) {

            SingletonManager.Remove<GetWaterManager>();
            SingletonManager.Register(this);
        }
        else
        {
            SingletonManager.Register(this);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize(); 
    }

    public override void Initialize()
    {
        id = Constants.GET_WATER_NAME;
        if(sceneChange == null)
        {
            sceneChange = this.GetComponent<SceneChange>();
        }
        transitionManager = SingletonManager.Get<TransitionManager>();
        isCompleted= false; 
        Events.OnObjectiveUpdate.Invoke();
        playerProgress = SingletonManager.Get<PlayerProgress>();
    }

   

    public void SetNumOfSwipes(int count)
    {
        NumOfSwipes = count;
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
        gameStartTimer = gameStartTime;
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());
    }

    protected override IEnumerator StartMinigameCounter()
    {
        //Disable Well UI 
        //Disable Swiping
        WaterWell well = wateringWell.GetComponent<WaterWell>();
        if (well)
        {
            well.UI.SetActive(false);
            well.CanSwipeDown = false;
        }
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
        //Activate well UI
        well.UI.SetActive(true);
        well.CanSwipeDown = true;
        Events.OnObjectiveUpdate.Invoke();
        

        //Count the attempt in player progress 
        if (playerProgress)
        {   
            playerProgress.getWaterTracker.totalTime = maxTimer;
            playerProgress.getWaterTracker.numOfAttempts += 1; 
        }
        Debug.Log("Refresh Score board");

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
        WaterWell well = wateringWell.GetComponent<WaterWell>();
        if (well)
        {
            well.UI.SetActive(false);
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

    public override void OnMinigameFinished()
    {
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }

    #endregion

    #region Minigame Checkers
    public override void OnWin()
    {
        if (!isCompleted)
        {
            this.previousVal = SingletonManager.Get<PlayerData>().storedMotivationData;
            SingletonManager.Get<PlayerData>().previousStoredMotivation = this.previousVal;
            SingletonManager.Get<PlayerData>().storedMotivationData -= motivationalCost;
            SingletonManager.Get<MiniGameTimer>().StopCountdownTimer();
            isCompleted = true;
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateGoodResult();

            //Check if the minigame is in the task list
            if (IsMinigameInTaskList())
            {
                SingletonManager.Get<PlayerData>().isGetWaterFinished = true;
            }
            
            if (playerProgress) 
            {
                playerProgress.getWaterTracker.numOfTimesCompleted += 1;
                playerProgress.getWaterTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
                playerProgress.getWaterTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
            }
            Debug.Log("Minigame complete");
        }
    }

    public override void OnMinigameLose()
    {
        SingletonManager.Get<MiniGameTimer>().StopCountdownTimer();
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();
        Debug.Log("Minigame lose");
        if (playerProgress)
        {
            playerProgress.getWaterTracker.numOfTimesFailed += 1;
            playerProgress.getWaterTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.getWaterTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
        }
    } 
    
    public void CheckIfComplete()
    {
        if (AreBucketsFull())
        {
            OnWin();
        }
        else
        {
            OnMinigameLose();
        }
    }

    public bool AreBucketsFull()
    {
        bool areBucketsFull = false;
        float totalWaterAmount = 0; 
        Assert.IsNotNull(wateringWell, "Watering well is null or is not set");
        WaterWell waterWell = wateringWell.GetComponent<WaterWell>();
        if(waterWell == null) { return areBucketsFull; }
        if(waterWell.waterBuckets.Count > 0)
        {
            for(int i = 0; i < waterWell.waterBuckets.Count; i++)
            {
                totalWaterAmount += waterWell.waterBuckets[i];
            }
            if(totalWaterAmount >= (waterWell.fillWaterBucket.maxWater * RequiredNumSwipes) / 2)
            {
                Debug.Log("Win " + totalWaterAmount + " " + (waterWell.fillWaterBucket.maxWater * RequiredNumSwipes) / 2 );
                areBucketsFull = true;
            }
            else
            {
                Debug.Log("Lose " + totalWaterAmount + " " + (waterWell.fillWaterBucket.maxWater * RequiredNumSwipes) / 2);
                areBucketsFull = false;
            }
        }
        return areBucketsFull;
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
    #endregion
}
