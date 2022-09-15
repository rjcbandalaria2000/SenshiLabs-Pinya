using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImHungryManager : MinigameManager
{
    [Header("Setup")]
    public SpawnManager SpawnManager;
    public int          NumOfIngredients;

    // Start is called before the first frame update
    void Start()
    {
        
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
