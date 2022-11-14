using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaterThePlantsManager : MinigameManager
{
    [Header("Game Objects")]
    public List<GameObject> Plants;
    public GameObject waterBucket;

    [Header("Values")]
    public int plantsWatered;
    public int numOfPlants;

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
        sceneChange = this.GetComponent<SceneChange>();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        transitionManager = SingletonManager.Get<TransitionManager>();
        playerProgress = SingletonManager.Get<PlayerProgress>();
        numOfPlants = Plants.Count;
        //Disable Player / Water Bucket controls 
        if(waterBucket != null)
        { 
            // Hide water bucket
            waterBucket.SetActive(false);
        }
        //Hide all plants
        HideAllPlants();
    }

    public void HideAllPlants()
    {
        if(Plants.Count <= 0) { return; }
        for(int i = 0; i < Plants.Count; i++)
        {
            Plants[i].SetActive(false);
        }
    }

    public void ShowAllPlants()
    {
        if (Plants.Count <= 0) { return; }
        for (int i = 0; i < Plants.Count; i++)
        {
            Plants[i].SetActive(true);
        }
    }

    public bool AreAllPlantsWatered()
    {
        if (Plants == null) { return false; }
        bool AllWatered = false;
        foreach (GameObject plant in Plants)
        {
            Plant plantObj = plant.GetComponent<Plant>();

            if (plantObj)
            {
                //Check all the plants if all is watered
                Debug.Log(plantObj.gameObject.name + " is watered? " + plantObj.IsWatered);
                AllWatered = plantObj.IsWatered;
                if (!AllWatered)
                {
                    //if at least one plant is not watered break the loop and return false
                    break;
                }
            }

        }
        return AllWatered;

    }

    #region Getter Functions

    public int GetRemainingPlants()
    {
        return numOfPlants - plantsWatered;
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
        //Deactivate Game UI
        SingletonManager.Get<UIManager>().DeactivateGameUI();
        //Deactivate Minigame Main Menu
        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();

        if (transitionManager != null)
        { 
            //Start Curtain Transition
            transitionManager.ChangeAnimation(TransitionManager.CURTAIN_OPEN);
            //Wait for the animation to finish
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
        SingletonManager.Get<UIManager>().ActivateGameUI();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();
        Events.OnObjectiveUpdate.Invoke();

        //Enable movement and input from bucket
        if (waterBucket != null)
        {
            waterBucket.SetActive(true);
            //Activate controls on the bucket
            WateringCan wateringCan = waterBucket.GetComponent<WateringCan>();
            if (wateringCan)
            {
                wateringCan.StartOnClickControls();
            }
        }

        //Show all plants
        ShowAllPlants();
        isCompleted = false;
        if (playerProgress)
        {
            playerProgress.waterThePlantsTracker.totalTime = maxTimer;
            playerProgress.waterThePlantsTracker.numOfAttempts += 1;
        }
        yield return null;
    }

    #endregion

    #region Finish Minigame Functions
    public override void CheckIfFinished()
    {
        if (AreAllPlantsWatered())
        {
            OnWin();
        }
    }

    public override void OnWin()
    {
        if (!isCompleted)
        {
            Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
            Debug.Log("Finished Watering the Plants");

            //Disable player bucket controls 
            if (waterBucket)
            {
                WateringCan wateringCan = waterBucket.GetComponent<WateringCan>();
                if (wateringCan)
                {
                    wateringCan.StopOnClickControls();
                    Debug.Log("Stop on click controls");
                }

                MouseFollow mouseFollow = waterBucket.GetComponent<MouseFollow>();
                if (mouseFollow)
                {
                    mouseFollow.enabled = false;
                }
            }
            
            SingletonManager.Get<MiniGameTimer>().StopCountdownTimer();
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateGoodResult();
            isCompleted = true;
            SingletonManager.Get<PlayerData>().isWaterThePlantsFinished = true;

            if (playerProgress)
            {
                playerProgress.waterThePlantsTracker.numOfTimesCompleted += 1;
                playerProgress.waterThePlantsTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
                playerProgress.waterThePlantsTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
            }
            //OnMinigameFinished();
        }
        
    }

    public override void OnMinigameLose()
    {
        Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();
        //Disable player bucket controls 
        if (waterBucket)
        {
            WateringCan wateringCan = waterBucket.GetComponent<WateringCan>();
            if (wateringCan)
            {
                wateringCan.StopOnClickControls();
            }

            MouseFollow mouseFollow = waterBucket.GetComponent<MouseFollow>();
            if (mouseFollow)
            {
                mouseFollow.enabled = false;
            }
        }
        if (playerProgress)
        {
            playerProgress.waterThePlantsTracker.numOfTimesFailed += 1;
            playerProgress.waterThePlantsTracker.timeRemaining = SingletonManager.Get<MiniGameTimer>().GetTimer();
            playerProgress.waterThePlantsTracker.timeElapsed = SingletonManager.Get<MiniGameTimer>().GetTimeElapsed();
        }
        Debug.Log("Minigame lose");
    }

    public override void OnMinigameFinished()
    {
        if (NameOfNextScene == null) { return; }
        Assert.IsNotNull(sceneChange, "Scene change is null or is not set");
        sceneChange.OnChangeScene(NameOfNextScene);
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
        if (transitionManager)
        {
            transitionManager.ChangeAnimation(TransitionManager.CURTAIN_CLOSE);
        }
        
        //Deactivate active UI 
        SingletonManager.Get<UIManager>().DeactivateResultScreen();
        SingletonManager.Get<UIManager>().DeactivateTimerUI();
        SingletonManager.Get<UIManager>().DeactivateGameUI();

        //Deactivate Game Objects 
        HideAllPlants();
        if (waterBucket)
        {
            waterBucket.SetActive(false);
        }
        //Wait for transition to end
        while (!transitionManager.IsAnimationFinished())
        {
            Debug.Log("Transition to closing");
            yield return null;
        }
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        if(NameOfNextScene != null)
        {
            sceneChange.OnChangeScene(NameOfNextScene);
        }
        
        yield return null;
    }

    #endregion
}
