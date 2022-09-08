using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTimer : MonoBehaviour
{
    private float timer;
    private float maxTimer;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        StartCoroutine(SingletonManager.Get<GameManager>().countdownTimer());
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

}
