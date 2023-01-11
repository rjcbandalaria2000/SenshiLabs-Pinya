using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [Header("State")]
    public bool                 isCompleted = false;

    [Header("Scene Change")]
    public string               NameOfNextScene;
    protected SceneChange       sceneChange;

    [Header("Timers")]
    public float                timer;
    public float                maxTimer;
    public float                speedTimer;

    [Header("Countdown Timer")]
    public float                gameStartTime = 3f;
    public DisplayGameCountdown countdownTimerUI;
    protected float             gameStartTimer = 0;
    protected Coroutine         startMinigameRoutine;
    protected Coroutine         exitMinigameRoutine;

    [Header("Costs")]
    public int motivationalCost = 20;

    [Header("Managers")]
    protected TransitionManager transitionManager;


    // Start is called before the first frame update
    void Start()
    {

    }

    public virtual void Initialize()
    {

    }

    public virtual void StartMinigame()
    {

    }

    public virtual void CheckIfFinished()
    {

    }

    public virtual void OnMinigameFinished()
    {

    }

    public virtual void OnMinigameLose()
    {

    }


    public virtual void OnWin()
    {

    }

    public virtual void OnLose()
    {

    }

    public virtual void OnExitMinigame()
    {

    }

    protected virtual IEnumerator StartMinigameCounter()
    {
        yield return null;
    }

    protected virtual IEnumerator ExitMinigame()
    {
        yield return null;
    }

    public virtual void GameMinigamePause()
    {

    }

    public virtual void GameMinigameResume()
    {

    }
}
