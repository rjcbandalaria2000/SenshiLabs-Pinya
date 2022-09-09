using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingMinigameManager : MinigameManager
{
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = SingletonManager.Get<SpawnManager>();
        spawnManager.StartTimedSpawnBoxSpawn();
    }

    public override void Initialize()
    {
        
    }

    public override void CheckIfFinished()
    {
        
    }
}
