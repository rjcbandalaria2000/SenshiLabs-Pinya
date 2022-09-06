using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTimer : MonoBehaviour
{
    public float timer;
    public float maxTimer;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        StartCoroutine(SingletonManager.Get<GameManager>().countdownTimer());
    }
}
