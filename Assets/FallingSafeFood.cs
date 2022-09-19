using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSafeFood : FallingFood
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnCollided(GameObject unit = null)
    {
        SingletonManager.Get<SleepingMinigameManager>().PlayerPoints++;
        SingletonManager.Get<SleepingMinigameManager>().CheckIfFinished();
    }

}
