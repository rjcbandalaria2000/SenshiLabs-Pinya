using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTimer : MonoBehaviour
{
    private float timer;
    private float maxTimer;

    private float speed;

    private HideSeekManager hideSeekManager;
    private void Awake()
    {
        SingletonManager.Register(this);

        speed = SingletonManager.Get<GameManager>().speedCounter;
        maxTimer = SingletonManager.Get<GameManager>().maxTime;
        hideSeekManager = GameObject.FindObjectOfType<HideSeekManager>().GetComponent<HideSeekManager>();
        timer = maxTimer;
    }

    private void Start()
    {
        StartCoroutine(countdownTimer());
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
        timer -= 1;
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
            hideSeekManager.checkChildren();
        }// Lose Condition
        yield return null;
    }
}

