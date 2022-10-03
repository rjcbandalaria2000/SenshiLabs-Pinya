using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaterThePlantsManager : MinigameManager
{
    [Header("Game Objects")]
    public List<GameObject> Plants;
    public GameObject waterBucket;

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

        //Disable Player / Water Bucket controls 
        if(waterBucket != null)
        {
            MouseFollow mouseFollow = waterBucket.GetComponent<MouseFollow>();
            if (mouseFollow)
            {
                mouseFollow.enabled = false;
            }
            WateringCan wateringCan = waterBucket.GetComponent<WateringCan>();
            if (wateringCan)
            {
                wateringCan.enabled = false;
            }
        }
    }

    public void HideAllPlants()
    {
        if(Plants.Count <= 0) { return; }
        
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
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();
        Events.OnObjectiveUpdate.Invoke();
        
        //Enable movement and input from bucket

        isCompleted = false;
        yield return null;
    }

    #endregion

    #region Finish Minigame Functions
    public override void CheckIfFinished()
    {
        if (AreAllPlantsWatered())
        {
            Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
            Debug.Log("Finished Watering the Plants");
            OnMinigameFinished();
        }
        else if (SingletonManager.Get<MiniGameTimer>().getTimer() <= 0)
        {
            Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
            Debug.Log("Fail Watering the Plants");
            OnMinigameFinished();
        }
    }

    public override void OnWin()
    {
        base.OnWin();
    }

    public override void OnLose()
    {
        base.OnLose();
    }

    public override void OnMinigameFinished()
    {
        if (NameOfNextScene == null) { return; }
        Assert.IsNotNull(sceneChange, "Scene change is null or is not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }
    #endregion
}
