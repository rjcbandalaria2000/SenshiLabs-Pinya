using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTimer : MonoBehaviour
{
    private float timer;
    private float maxTimer;

    private float speed;
    public float decreaseValue;

    private MinigameManager miniGames;
    private Coroutine countdownTimerRoutine;

    private void Awake()
    {
        SingletonManager.Register(this);

       

        if(miniGames == null)
        {
            if(GameObject.FindObjectOfType<MinigameManager>() != null)
            {
                miniGames = GameObject.FindObjectOfType<MinigameManager>().GetComponent<MinigameManager>();
            }
            
        }
        speed = miniGames.speedTimer;
        maxTimer = miniGames.maxTimer;

        timer = maxTimer;
    }

    private void Start()
    {
        decreaseValue = 1;
        //StartCoroutine(countdownTimer());
    }

    public void StartCountdownTimer()
    {
        countdownTimerRoutine = StartCoroutine(CountdownTimer());
    }

    public void StopCountdownTimer()
    {
        if(countdownTimerRoutine == null) { return; }
        StopCoroutine(countdownTimerRoutine);
    }

    public float GetTimer()
    {
        return timer;
    }

    public float SetTimer(float value)
    {
        timer += value;
        return timer;
    }

    public float GetMaxTimer()
    {
        return maxTimer;
    }

    public float SetMaxTimer(float value)
    {
        maxTimer += value;
        return maxTimer;
    }

    public float CountdownMinigame()
    {
        timer -= decreaseValue;
        return timer;
    }

    public float GetTimeRemaining()
    {
        return timer;
    }

    public float GetTimeElapsed()
    {
        return maxTimer - timer;
    }
  
    public IEnumerator CountdownTimer()
    {
        while (timer > 0)
        {
            CountdownMinigame();
            //SingletonManager.Get<DisplayMiniGameTimer>().updateMiniGameTimer();
            Events.OnDisplayMinigameTime.Invoke();
            yield return new WaitForSeconds(speed);
        }
        if( timer <= 0)
        {
            if (miniGames != null)
            {
                miniGames.OnMinigameLose();
            }           
        }// Lose Condition
        yield return null;
    }
}

