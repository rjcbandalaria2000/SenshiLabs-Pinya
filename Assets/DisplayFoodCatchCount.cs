using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;

public class DisplayFoodCatchCount : MonoBehaviour
{
    public TextMeshProUGUI CatchCount;
    public SleepingMinigameManager SleepingMinigameManager;
    // Start is called before the first frame update
    void Start()
    {
        //if(CatchCount == null)
        //{
        //    CatchCount = this.GetComponent<TextMeshProUGUI>();
        //}
        CatchCount = this.GetComponent<TextMeshProUGUI>();
        if (SleepingMinigameManager == null)
        {
            SleepingMinigameManager = SingletonManager.Get<SleepingMinigameManager>();
        }
        Events.UpdateScore.AddListener(UpdateCatchCount);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void UpdateCatchCount()
    {
        Assert.IsNotNull(CatchCount, "Catch Count text is not set or is null");
        Assert.IsNotNull(SleepingMinigameManager, "Sleeping Minigame Manager is null or is not set");
        CatchCount.text = SleepingMinigameManager.PlayerPoints.ToString();
    }

    public void OnSceneChange()
    {
        Events.UpdateScore.RemoveListener(UpdateCatchCount);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
    
}
