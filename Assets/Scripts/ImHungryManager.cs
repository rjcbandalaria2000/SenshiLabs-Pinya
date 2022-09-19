using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ImHungryManager : MinigameManager
{
    [Header("Setup")]
    public SpawnManager     SpawnManager;
    public int              NumOfIngredients;
    public List<GameObject> IngredientsToSpawn;
    public GameObject       Pot;

    private Pot pot;
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneChange = this.GetComponent<SceneChange>();
        Assert.IsNotNull(Pot, "Pot is null or is not set");
        SpawnManager = SingletonManager.Get<SpawnManager>();
        //SpawnManager.ObjectToSpawn = IngredientsToSpawn;
        SpawnManager.SpawnRandomObjectsInStaticPositions();
        pot = Pot.GetComponent<Pot>();
        Events.OnObjectiveComplete.AddListener(CheckIfFinished);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void CheckIfFinished()
    {
        if(pot == null) { return; }
        if (pot.IsCooked)
        {
            Debug.Log("Finished Cooking");
            Assert.IsNotNull(sceneChange, "Scene change is null or is not set");
            if(NameOfNextScene == null) { return; }
            sceneChange.OnChangeScene(NameOfNextScene);
        }
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
