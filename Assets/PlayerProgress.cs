using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScoreTracker
{
    public float score;
    public float time;
    public int numOfTimesCompleted;
    public int numOfTimesFailed;
    public int numOfAttempts;
}

public class PlayerProgress : MonoBehaviour
{
    [Header("Tasks Trackers")]
    public ScoreTracker cleanTheHouseTracker;
    public ScoreTracker washTheDishesTracker;
    public ScoreTracker groceryTracker;
    public ScoreTracker waterThePlantsTracker;
    public ScoreTracker getWaterTracker;
    public ScoreTracker imHungryTracker;
    public ScoreTracker foldTheClothesTracker;

    [Header("Motivational Trackers")]
    public ScoreTracker tagTracker;
    public ScoreTracker sleepTracker;
    public ScoreTracker hideSeekTracker;
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

   
}
