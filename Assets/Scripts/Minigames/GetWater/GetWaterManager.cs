using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GetWaterManager : MinigameManager
{
    [Header("Well")]
    public GameObject   wateringWell;

    [Header("Water UI")]
    public Slider       slider;

    [Header("Setup Values")]
    public int          RequiredNumSwipes = 3;
    public int          NumOfSwipes = 0;

    private void Awake()
    {
        //Trying this solution since
        //every destroy or load the singleton must be destroyed
        //and replaced with a new singeleton script when it exists
        GetWaterManager getWaterManager = SingletonManager.Get<GetWaterManager>();
        if (getWaterManager != null) {

            SingletonManager.Remove<GetWaterManager>();
            SingletonManager.Register(this);
        }
        else
        {
            SingletonManager.Register(this);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize(); 
    }

    public override void Initialize()
    {
        if(sceneChange == null)
        {
            sceneChange = this.GetComponent<SceneChange>();
        }
        Events.OnObjectiveUpdate.Invoke();
    }

   

    public void SetNumOfSwipes(int count)
    {
        NumOfSwipes = count;
    }

    

    #region Starting Minigame Functions

    public override void StartMinigame()
    {
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());
    }
    protected override IEnumerator StartMinigameCounter()
    {
        return base.StartMinigameCounter();
    }

    #endregion

    #region Exit Minigame Functions

    public override void OnExitMinigame()
    {
        base.OnExitMinigame();
    }

    protected override IEnumerator ExitMinigame()
    {
        return base.ExitMinigame();
    }

    public override void OnMinigameFinished()
    {
        base.OnMinigameFinished();
    }

    #endregion

    #region Minigame Checkers
    public override void OnWin()
    {
        Debug.Log("All buckets are full");

    }

    public override void OnMinigameLose()
    {
        base.OnMinigameLose();
    } 
    
    public void CheckIfComplete()
    {
        if (AreBucketsFull())
        {
            OnWin();
        }
        else
        {
            OnMinigameLose();
        }
        //if(NumOfSwipes == RequiredNumSwipes)
        //{
        //    Debug.Log("Congratulations! You managed to get water");
        //    SingletonManager.Get<PlayerData>().IsGetWaterFinished = true;
        //}
        //else if(NumOfSwipes < RequiredNumSwipes)
        //{
        //    Debug.Log("Try again next time");
        //}
        //else if(NumOfSwipes > RequiredNumSwipes)
        //{
        //    Debug.Log("Whoops you broke the rope, try again");
        //}
        //if (sceneChange)
        //{
        //    Events.OnSceneChange.Invoke();
        //    sceneChange.OnChangeScene(NameOfNextScene);
        //}

    }

    public bool AreBucketsFull()
    {
        bool areBucketsFull = false;
        Assert.IsNotNull(wateringWell, "Watering well is null or is not set");
        WaterWell waterWell = wateringWell.GetComponent<WaterWell>();
        if(waterWell == null) { return areBucketsFull; }
        if(waterWell.waterBuckets.Count > 0)
        {
            for(int i = 0; i < waterWell.waterBuckets.Count; i++)
            {
                if (waterWell.waterBuckets[i] < waterWell.fillWaterBucket.maxWater)
                {
                    Debug.Log("Insufficient water amount");
                    areBucketsFull = false;
                    break;
                }
                else
                {
                    areBucketsFull = true;
                }
            }
        }
        return areBucketsFull;
    }
    #endregion
}
