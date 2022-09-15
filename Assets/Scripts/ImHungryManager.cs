using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImHungryManager : MinigameManager
{
    [Header("Setup")]
    public SpawnManager SpawnManager;
    public int          NumOfIngredients;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnManager = SingletonManager.Get<SpawnManager>();
        SpawnManager.SpawnRandomObjectsInStaticPositions();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void CheckIfFinished()
    {
        base.CheckIfFinished();
    }

    public override void OnWin()
    {
        base.OnWin();
    }

    public override void OnLose()
    {
        base.OnLose();
    }
}
