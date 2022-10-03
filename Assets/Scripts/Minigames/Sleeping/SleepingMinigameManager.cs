using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        spawnManager = SingletonManager.Get<SpawnManager>();
       
      
      //  Events.OnObjectiveUpdate.Invoke();
        Events.UpdateScore.Invoke();
    }

    private void Update()
    {
        if(SingletonManager.Get<MiniGameTimer>().getTimer() <= 0)
        {
            CheckIfFinished();
        }
    }

    public override void Initialize()
    {

        startMinigameRoutine = StartCoroutine(StartMinigameCounter());

        
        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();

  
        sceneChange = this.gameObject.GetComponent<SceneChange>();

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
            SingletonManager.Remove<SleepingMinigameManager>();
            SingletonManager.Remove<SpawnManager>();
            if(sceneChange == null) { return; }
            if(NameOfNextScene == null) { return; }
            Events.OnSceneChange.Invoke();
            sceneChange.OnChangeScene(NameOfNextScene);

        }
        else if(PlayerPoints <= RequiredPoints && SingletonManager.Get<MiniGameTimer>().getTimer() <= 0)
        {
            spawnManager.StopTimedUnlimitedSpawnBox();
            Debug.Log("Minigame Fail");
            SingletonManager.Remove<SleepingMinigameManager>();
            SingletonManager.Remove<SpawnManager>();
            if (sceneChange == null) { return; }
            if (NameOfNextScene == null) { return; }
            Events.OnSceneChange.Invoke();
            sceneChange.OnChangeScene(NameOfNextScene);
           
        }
    }

   

    public void GetPlayerPoints(int points)
    {

    }

    public override void OnMinigameFinished()
    {
       
    }

    public override void OnWin()
    {
        
    }

    public override void OnLose()
    {
       
    }

    protected override IEnumerator StartMinigameCounter()
    {
        gameStartTimer = GameStartTime;

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

}
