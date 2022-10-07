using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoldingMinigameManager : MinigameManager
{

    public Clothes ClothesComponent;

    [Header("Countdown Timer")]
    public float GameStartTime = 3f;
    public DisplayGameCountdown CountdownTimerUI;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
       
        Initialize();

        if (ClothesComponent == null)
        {
            if(GameObject.FindObjectOfType<Clothes>() != null)
            {
                ClothesComponent = GameObject.FindObjectOfType<Clothes>().GetComponent<Clothes>();
            }
        }

        //  Events.OnObjectiveUpdate.Invoke();
        Events.UpdateScore.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if(SingletonManager.Get<MiniGameTimer>().GetTimer() <= 0)
        {
            CheckIfFinished();
        }
    }

    public override void Initialize()
    {
        transitionManager = SingletonManager.Get<TransitionManager>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
       
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

    public override void StartMinigame()
    {
       


        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        ClothesComponent.gameObject.SetActive(false);
     

        startMinigameRoutine = StartCoroutine(StartMinigameCounter());

        ClothesComponent.gameObject.SetActive(true);

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

        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();



        Events.OnObjectiveUpdate.Invoke();
        Debug.Log("Refresh Score board");
        //Spawn objects

        isCompleted = false;
    }


    public override void CheckIfFinished()
    {
        if (ClothesComponent.clothes == 0)
        {
            Debug.Log("Minigame complete");
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateGoodResult();
            SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
        }
        else
        {
            Debug.Log("Minigame lose");
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateGoodResult();
            SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
        }

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

}
