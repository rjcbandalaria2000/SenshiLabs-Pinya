using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
    public float                    earnedMotivationalValue = 20f;

    private int                     RNG;
    private PlayerProgress          playerProgress;
    private PlayerData              playerData;

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
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        playerProgress = SingletonManager.Get<PlayerProgress>();
        playerData = SingletonManager.Get<PlayerData>();
        SingletonManager.Get<UIManager>().ActivateMiniGameMainMenu();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        Events.OnSceneChange.AddListener(OnSceneChange);
        startMinigameRoutine = null;

    }

    IEnumerator spawn()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            List<GameObject> list = new List<GameObject>();
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
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateGoodResult();
        SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
        IncreaseMotivationMeter(earnedMotivationalValue);
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
            playerData.storedMotivationData += motivationValue;
        }
    }
}
