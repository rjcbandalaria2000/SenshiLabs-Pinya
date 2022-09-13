using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashTheDishesManager : MinigameManager
{
    [Header("Values")]
    public int NumOfCleanPlates;
    public int NumOfDirtyPlates;

    [Header("Plate Positions")]    
    public GameObject WashingPosition;
    public List<GameObject> DirtyPile;
    public List<GameObject> CleanPile;


    private void Awake()
    {
        SingletonManager.Register(this);
    }
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

    public override void OnMinigameFinished()
    {
        base.OnMinigameFinished();
    }

    public override void OnWin()
    {
        base.OnWin();
    }

    public override void OnMinigameLose()
    {
        base.OnMinigameLose();
    }

    public override void OnLose()
    {
        base.OnLose();
    }

    public int GetRemainingDirtyPlates()
    {
        return NumOfDirtyPlates - NumOfCleanPlates;
    }
}
