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
        countdownTimerRoutine = StartCoroutine(countdownTimer());
    }

    public void StopCountdownTimer()
    {
        if(countdownTimerRoutine == null) { return; }
        StopCoroutine(countdownTimerRoutine);
    }

    public float getTimer()
    {
        return timer;
    }

    public float setTimer(float value)
    {
        timer += value;
        return timer;
    }

    public float getMaxTimer()
    {
        return maxTimer;
    }

    public float setMaxTimer(float value)
    {
        maxTimer += value;
        return maxTimer;
    }

    public float countDown_Minigame()
    {
        timer -= decreaseValue;
        return timer;
    }
  
    public IEnumerator countdownTimer()
    {
        while (timer > 0)
        {
            countDown_Minigame();
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

