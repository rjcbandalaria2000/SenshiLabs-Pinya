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

    public Player                   player;

    [Header("MiniGame Manager")]
    public List<MinigameObject>     minigames = new();
    
    private UIManager               UI;
    private TransitionManager       transitionManager;
    private Coroutine               startGameRoutine;
    private SceneChange             sceneChange;

    private void Awake()
    {
        SingletonManager.Register(this);
        sceneChange = this.GetComponent<SceneChange>();
        
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
        StartGameTransition();
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
        //Activate Lose Panel
        UI.ActivateLosePanel();

        //Disable Game UI
        UI.DeactivateGameUI();
    }

    public void GameWin()
    {
        if (!isGameFinished)
        {
            isGameFinished = true;
            Debug.Log("Player wins");
            SingletonManager.Get<UIManager>().ActivateWinPanel();
            sceneChange.OnChangeScene("EndingStoryboard");
        }
    }

    public void CheckEndingCondition()
    {

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

}