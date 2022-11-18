using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [Header ("Meters")]
    public MotivationMeter          playerMotivation;
    public PinyaMeter               playerPinyaMeter;

    [Header("States")]
    public bool isGameFinished = false;

    [Header("Change Values")]
    public float                    MotivationValueChange;
    public float                    PinyaMeterValueChange;

    [Header("Mini-Game Timer")]
    public float                    currentTime;
    public float                    maxTime;
    public float                    speedCounter;

    [Header("Player")]
    public Player                   player;

    [Header("Ending Scenes")]
    public string                   goodEndingSceneName;
    public string                   badEndingSceneName;   

    //[Header("MiniGame Manager")]
    //public List<MinigameObject>     minigames = new();
    
    private UIManager               UI;
    private TransitionManager       transitionManager;
    private Coroutine               startGameRoutine;
    private SceneChange             sceneChange;
    private PlayerProgress          playerProgress;

    public GameObject tutorial;

    private void Awake()
    {
        SingletonManager.Register(this);
        sceneChange = this.GetComponent<SceneChange>();
        playerProgress = SingletonManager.Get<PlayerProgress>();
        currentTime = maxTime;
        Events.OnPinyaEmpty.AddListener(GameLose);
        Events.OnSceneChange.AddListener(OnSceneChange);
        Events.OnTasksComplete.AddListener(GameWin);
    }
    public void Start()
    {
        if(UI == null)
        {
            if(GameObject.FindObjectOfType<UIManager>() != null)
            {
                UI = GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>();
            }
        }
        else
        {
            Debug.Log("Null UI");
        }

        if (player == null)
        {
            if (GameObject.FindObjectOfType<Player>() != null)
            {
                player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
            }

        }

        if(transitionManager == null)
        {
            transitionManager = SingletonManager.Get<TransitionManager>();
        }
      
        if(SingletonManager.Get<PlayerData>().firstTime == true)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
            StartGameTransition();
        }

       
       // StartGameTransition();
    }

    public void StartGameTransition()
    {
        // for curtain transition
        startGameRoutine = StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        //Disable player controls
        PlayerControls playerControls = player.gameObject.GetComponent<PlayerControls>();
        if (playerControls)
        {
            playerControls.enabled = false;
        }
        //Disable UI 
        UI.DeactivateGameUI();
        transitionManager.stateOfDayGO.SetActive(true);
        transitionManager.stateDayTransition.NextState(SingletonManager.Get<DayCycle>().timePeriod);
        while (!transitionManager.stateDayTransition.isFinished)
        {
            yield return null;
        }
        transitionManager.stateOfDayGO.SetActive(false);
        // Start playing curtain animation 
        transitionManager.ChangeAnimation(TransitionManager.CURTAIN_OPEN);

        //Wait for the animation to finish 
        while (!transitionManager.IsAnimationFinished())
        {
            yield return null;
        }
        //Display UI
        UI.ActivateGameUI();

        //Activate player controls 
        playerControls.enabled = true;
        yield return null;
    }

    public void GameLose()
    {

        OnSceneChange();
        GoToEnding(badEndingSceneName);

        ////Activate Lose Panel
        //UI.ActivateLosePanel();

        ////Disable Game UI
        //UI.DeactivateGameUI();
    }

    public void GameWin()
    {
        if (!isGameFinished)
        {
            isGameFinished = true;
            Debug.Log("Player wins");
            CheckEndingCondition();
            //SingletonManager.Get<UIManager>().ActivateWinPanel();
            //sceneChange.OnChangeScene("EndingStoryboard");
        }
    }

    public void CheckEndingCondition()
    {
        if (playerProgress)
        {
            if(playerProgress.GetTotalTimeElapsed() <= playerProgress.GetAllTotalTime())
            {
                GoToEnding(goodEndingSceneName);
                Debug.Log("Get the good ending");
            }
            else
            {
                GoToEnding(badEndingSceneName);
                Debug.Log("Get the bad ending ");
            }
        }
    }

    public void GoToEnding(string endingSceneName)
    {
        Assert.IsNotNull(sceneChange, "Scene change is not set or is null");
        OnSceneChange();
        sceneChange.OnChangeScene(endingSceneName);
    }

    public void OnSceneChange()
    {
        //Remove Active singletons 
        //SingletonManager.Remove<GameManager>();
        //SingletonManager.Remove<UIManager>();

        //Remove all listeners when scene changes
        Events.OnPinyaEmpty.RemoveListener(GameLose);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        Events.OnTasksComplete.RemoveListener(GameWin);
    }

    public void tutorialPlayButton()
    {
        tutorial.SetActive(false);
        SingletonManager.Get<PlayerData>().firstTime = false;

        StartGameTransition();
    }
}