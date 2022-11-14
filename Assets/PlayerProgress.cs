using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScoreTracker
{
    public float totalTime;
    public float timeRemaining;
    public float timeElapsed;
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

    public float GetAverageTimeElapsed()
    {
        float avgTimeElapsed = 0f;

        float totalTimeElapsed = cleanTheHouseTracker.timeElapsed + washTheDishesTracker.timeElapsed + groceryTracker.timeElapsed +
            groceryTracker.timeElapsed + waterThePlantsTracker.timeElapsed + getWaterTracker.timeElapsed +
            imHungryTracker.timeElapsed + foldTheClothesTracker.timeElapsed;

        avgTimeElapsed = totalTimeElapsed / Constants.NUM_OF_MAIN_TASKS;

        return avgTimeElapsed;
    }

    public float GetTotalTimeElapsed()
    {
        float totalTimeElapsed = cleanTheHouseTracker.timeElapsed + washTheDishesTracker.timeElapsed + groceryTracker.timeElapsed +
            groceryTracker.timeElapsed + waterThePlantsTracker.timeElapsed + getWaterTracker.timeElapsed +
            imHungryTracker.timeElapsed + foldTheClothesTracker.timeElapsed;

        return totalTimeElapsed;
    }

    public float GetAllTotalTime()
    {
        float totalTime = cleanTheHouseTracker.totalTime + washTheDishesTracker.totalTime + groceryTracker.totalTime +
            groceryTracker.totalTime + waterThePlantsTracker.totalTime + getWaterTracker.totalTime +
            imHungryTracker.totalTime + foldTheClothesTracker.totalTime;

        return totalTime;
    }

    

   
}
