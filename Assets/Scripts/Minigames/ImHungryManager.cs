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
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.GetComponent<SceneChange>();
        Assert.IsNotNull(Pot, "Pot is null or is not set");
        SpawnManager = SingletonManager.Get<SpawnManager>();
        //SpawnManager.ObjectToSpawn = IngredientsToSpawn;
        pot = Pot.GetComponent<Pot>();
        Events.OnObjectiveComplete.AddListener(CheckIfFinished);
    }

    public override void Initialize()
    {
        base.Initialize();
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
    }

    #endregion


    #region Exit Minigame Functions



    #endregion

    #region Minigame Checker Functions

    public override void CheckIfFinished()
    {
        if (pot == null) { return; }
        if (pot.IsCooked)
        {
            Debug.Log("Finished Cooking");
            Assert.IsNotNull(sceneChange, "Scene change is null or is not set");
            if (NameOfNextScene == null) { return; }
            sceneChange.OnChangeScene(NameOfNextScene);
        }
    }

    public override void OnWin()
    {
        base.OnWin();
    }

    public override void OnMinigameLose()
    {
        base.OnMinigameLose();
    }

    public override void OnMinigameFinished()
    {
        base.OnMinigameFinished();
    }

    #endregion

}
