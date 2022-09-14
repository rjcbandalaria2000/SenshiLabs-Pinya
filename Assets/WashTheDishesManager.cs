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
    private Coroutine nextPlateToWashRoutine;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        sceneChange = this.GetComponent<SceneChange>();
        plateToWashAreaRoutine = null;
        spawnManager = SingletonManager.Get<SpawnManager>();
        Assert.IsNotNull(spawnManager, "Spawn manager is null or is not set");
        spawnManager.NumToSpawn.Add(DirtyPilePosition.Count);
        spawnManager.SpawnPoints = DirtyPilePosition;
        spawnManager.SpawnInStaticPositions();
        Plates = spawnManager.SpawnedObjects;
        StartPlateToWashArea();
        Events.OnObjectiveUpdate.AddListener(StartNextPlate);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void CheckIfFinished()
    {
        if(GetRemainingDirtyPlates() <= 0)
        {
            OnWin();
        }
    }

    public override void OnWin()
    {
        Debug.Log("You cleaned all the plates");
        Assert.IsNotNull(sceneChange, "Scene change is null or is not set");
        if(NameOfNextScene == null) { return; }
        Events.OnObjectiveUpdate.RemoveListener(StartNextPlate);
        Events.OnSceneChange.Invoke();
        sceneChange.OnChangeScene(NameOfNextScene);

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
        Plates[plateIndex].transform.position = CleanPilePosition[plateIndex].transform.position;
    }

    public void StartPlateToWashArea()
    {
        plateToWashAreaRoutine = StartCoroutine(PlateToWashArea());
    }

    IEnumerator PlateToWashArea()
    {
        yield return new WaitForSeconds(0.5f);
        GoToWashArea();
        Plate selectedPlate = Plates[plateIndex].GetComponent<Plate>();
        if (selectedPlate)
        {
            selectedPlate.CanClean = true;
        }


    }

    public void StartNextPlate()
    {
        nextPlateToWashRoutine = StartCoroutine(NextPlateToWash());
    }

    IEnumerator NextPlateToWash()
    {
        NumOfCleanPlates++;
        yield return new WaitForSeconds(0.5f);
        GoToCleanPile();
        CheckIfFinished();
        plateIndex++;
        yield return new WaitForSeconds(0.5f);
        if (plateIndex < Plates.Count)
        {
            StartPlateToWashArea();
        }
    }

    
}
