using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WashTheDishesManager : MinigameManager
{
    [Header("Values")]
    public int              NumOfCleanPlates;
    public int              NumOfDirtyPlates;

    [Header("Plate Positions")]    
    public GameObject       WashingPosition;
    public List<GameObject> DirtyPilePosition;
    public List<GameObject> CleanPilePosition;

    [Header("Spawned Objects")]
    public List<GameObject> Plates = new();

    private SpawnManager    spawnManager;
    private int plateIndex = 0;
    private Coroutine plateToWashAreaRoutine;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        plateToWashAreaRoutine = null;
        spawnManager = SingletonManager.Get<SpawnManager>();
        Assert.IsNotNull(spawnManager, "Spawn manager is null or is not set");
        spawnManager.NumToSpawn.Add(DirtyPilePosition.Count);
        spawnManager.SpawnPoints = DirtyPilePosition;
        spawnManager.SpawnInStaticPositions();
        Plates = spawnManager.SpawnedObjects;
        plateToWashAreaRoutine = StartCoroutine(PlateToWashArea());
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

    public void GoToWashArea()
    {
        Plates[plateIndex].transform.position = WashingPosition.transform.position;
    }

    public void GoToCleanPile()
    {

    }

    IEnumerator PlateToWashArea()
    {
        yield return new WaitForSeconds(0.5f);
        GoToWashArea();
        plateIndex++;
    }

    
}
