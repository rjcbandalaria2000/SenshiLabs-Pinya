using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class TagManager : MinigameManager
{
    public GameObject player;
    public GameObject spawnedPlayer;

    [Header("AI")]
    public List<GameObject> activeAI;
    public List<Transform>  points;

    [Header("SpawnPos")]
    public Transform playerPos;
    
    public UIManager uIManager;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    void Start()
    {
        Initialize();

    }

    public override void Initialize()
    {
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();

        uIManager.ActivateMiniGameMainMenu();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        Events.OnSceneChange.AddListener(OnSceneChange);
        startMinigameRoutine = null;

    }

    private void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
        Events.OnSceneChange.RemoveListener(OnSceneChange);

    }

    public override void StartMinigame()
    {

        startMinigameRoutine = StartCoroutine(StartMinigameCounter());

    }

    protected override IEnumerator StartMinigameCounter()
    {
        gameStartTimer = gameStartTime;

        //Deactivate Minigame Main Menu
        uIManager.DeactivateMiniGameMainMenu();
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
        uIManager.ActivateGameCountdown();
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

        startMinigameRoutine = StartCoroutine(initializeMiniGame());

        uIManager.DeactivateGameCountdown();
        uIManager.ActivateGameUI();
        uIManager.ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();

        StartCoroutine(checkStatus());

        //Debug.Log("Refresh Score board");
        //Spawn objects

        isCompleted = false;
    }

    public override void CheckIfFinished()
    {
        if (spawnedPlayer.GetComponent<TagMinigamePlayer>().isTag == false)
        {
            Debug.Log("Minigame complete");
            isCompleted = true;
            uIManager.ActivateResultScreen();
            uIManager.ActivateGoodResult();
            SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
            spawnedPlayer.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Minigame lose");
            isCompleted = true;
            uIManager.ActivateResultScreen();
            uIManager.ActivateBadResult();
            SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
            spawnedPlayer.gameObject.SetActive(false);
        }

    }
    IEnumerator initializeMiniGame()
    {
        GameObject newPlayer = Instantiate(player.gameObject, playerPos.position, Quaternion.identity);
        spawnedPlayer = newPlayer.GetComponent<TagMinigamePlayer>().gameObject;

        yield return null;
        for (int i = 0; i < activeAI.Count; i++) //initialze bot
        {
            if (i == 0)
            {
                activeAI[i].gameObject.SetActive(true);
                activeAI[i].GetComponent<AITagMinigame>().isTag = true;
                activeAI[i].GetComponent<AITagMinigame>().ID = i;
            }
            else
            {
                activeAI[i].gameObject.SetActive(true);
                activeAI[i].GetComponent<AITagMinigame>().isTag = false;
                activeAI[i].GetComponent<AITagMinigame>().ID = i;
              
            }
            yield return null;
        }

        startMinigameRoutine = null;

    }

    IEnumerator checkStatus()
    {

        while (!isCompleted)
        {
            if (SingletonManager.Get<MiniGameTimer>().GetTimer() <= 0)
            {

                for (int i = 0; i < activeAI.Count; i++)
                {
                    activeAI[i].SetActive(false);
                    spawnedPlayer.gameObject.SetActive(false);
                }
                CheckIfFinished();

                yield return null;
            }

            yield return null;
        }

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
        transitionManager.ChangeAnimation(TransitionManager.CURTAIN_CLOSE);
        //Deactivate active UI 
        uIManager.DeactivateResultScreen();
        uIManager.DeactivateTimerUI();
        uIManager.DeactivateGameUI();
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