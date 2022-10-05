using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

public class WashTheDishesManager : MinigameManager
{
    [Header("Values")]
    public int              numOfCleanPlates;
    public int              numOfDirtyPlates = 5;

    [Header("DoTween Animation Time")]
    public float            nextPlateDelay = 0.5f;
    public float            plateAnimationDuration = 0.5f;

    [Header("Plate Positions")]    
    public GameObject       washingPosition;
    public List<GameObject> dirtyPilePosition;
    public List<GameObject> cleanPilePosition;

    [Header("Spawned Objects")]
    public List<GameObject> plates = new();

    [Header("Sponge")]
    public GameObject       sponge;

    private SpawnManager    spawnManager;
    private int             plateIndex = 0;
    private Coroutine       plateToWashAreaRoutine;
    private Coroutine       nextPlateToWashRoutine;

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
        transitionManager = SingletonManager.Get<TransitionManager>();
        plateToWashAreaRoutine = null;
        spawnManager = SingletonManager.Get<SpawnManager>();
        Assert.IsNotNull(spawnManager, "Spawn manager is null or is not set");
        //Disable sponge at start 
        if (sponge)
        {
            sponge.SetActive(false);
        }
    }

    public void GoToWashArea()
    {
        plates[plateIndex].transform.DOMove(washingPosition.transform.position, plateAnimationDuration, false);
        //plates[plateIndex].transform.position = washingPosition.transform.position;
    }

    public void GoToCleanPile()
    {
        plates[plateIndex].transform.DOMove(cleanPilePosition[plateIndex].transform.position, plateAnimationDuration, false);
        plates[plateIndex].transform.DORotate(new Vector3(0, 0, 90), plateAnimationDuration, RotateMode.Fast);
        //plates[plateIndex].transform.position = cleanPilePosition[plateIndex].transform.position;
    }

    public void StartPlateToWashArea()
    {
        plateToWashAreaRoutine = StartCoroutine(PlateToWashArea());
    }

    IEnumerator PlateToWashArea()
    {
        yield return new WaitForSeconds(nextPlateDelay);
        GoToWashArea();
        Plate selectedPlate = plates[plateIndex].GetComponent<Plate>();
        if (selectedPlate)
        {
            selectedPlate.CanClean = true;
        }
    }

    public void StartNextPlate()
    {
        nextPlateToWashRoutine = StartCoroutine(NextPlateToWash());
    }

    IEnumerator NextPlateToWash()
    {
        numOfCleanPlates++;
        yield return new WaitForSeconds(0.5f);
        GoToCleanPile();
        CheckIfFinished();
        plateIndex++;
        yield return new WaitForSeconds(0.5f);
        if (plateIndex < plates.Count)
        {
            StartPlateToWashArea();
        }
    }

    public void HideAllPlates()
    {
        if(plates.Count <= 0) { return; }
        for(int i = 0; i < plates.Count; i++)
        {
            plates[i].SetActive(false);
        }
    }

    #region Starting Minigame Function
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

        //Begin game 
        // Show sponge
        if (sponge)
        {
            sponge.SetActive(true);
        }
        spawnManager.NumToSpawn.Add(dirtyPilePosition.Count);
        spawnManager.SpawnPoints = dirtyPilePosition;
        spawnManager.SpawnInStaticPositions();
        plates = spawnManager.SpawnedObjects;
        StartPlateToWashArea();
        Events.OnObjectiveUpdate.AddListener(StartNextPlate);



        isCompleted = false;
        yield return null;
    }

    #endregion

    #region Finish Minigame Functions
    public override void CheckIfFinished()
    {
        if (GetRemainingDirtyPlates() <= 0)
        {
            OnWin();
        }
    }

    public override void OnWin()
    {
        Debug.Log("You cleaned all the plates");
        Events.OnObjectiveUpdate.RemoveListener(StartNextPlate);
        //Stop timer 
        SingletonManager.Get<MiniGameTimer>().StopCountdownTimer();
        //Show result screen
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateGoodResult();
        isCompleted = true;
        SingletonManager.Get<PlayerData>().IsWashTheDishesFinished = true;

    }

    public override void OnMinigameLose()
    {
        Events.OnObjectiveUpdate.RemoveListener(StartNextPlate);
        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();
        //Disable controls
        DragAndDrop spongeControls = sponge.GetComponent<DragAndDrop>();
        if (spongeControls)
        {
            spongeControls.enabled = false;
        }
    }

    #endregion

    #region Exit Minigame Functions

    public override void OnExitMinigame()
    {
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }

    protected override IEnumerator ExitMinigame()
    {

        //Hide the remaining plates 
        HideAllPlates();
        if (sponge)
        {
            sponge.SetActive(false);
        }
        // Play close animation
        if (transitionManager)
        {
            transitionManager.ChangeAnimation(TransitionManager.CURTAIN_CLOSE);
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
        if (NameOfNextScene != null)
        {
            sceneChange.OnChangeScene(NameOfNextScene);
        }

        yield return null;
    }

    #endregion

    #region Getters
    public int GetRemainingDirtyPlates()
    {
        return numOfDirtyPlates - numOfCleanPlates;
    }

    #endregion

}
