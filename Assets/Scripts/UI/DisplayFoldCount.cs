using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class DisplayFoldCount : MonoBehaviour
{
    public TextMeshProUGUI FoldCount;
    public FoldingMinigameManager FoldMinigameManager;

    private void Awake()
    {
        SingletonManager.Register(this);

        if (FoldCount == null)
        {
            FoldCount = this.GetComponent<TextMeshProUGUI>();
        }
        
    }

    void Start()
    {
        if (FoldMinigameManager == null)
        {
            FoldMinigameManager = SingletonManager.Get<FoldingMinigameManager>();
        }
       
        //  Events.OnObjectiveUpdate.AddListener(UpdateCatchCount);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void UpdateFoldCount()
    {
        Assert.IsNotNull(FoldCount, "Catch Count text is not set or is null");
        Assert.IsNotNull(FoldMinigameManager, "Fold Minigame Manager is null or is not set");
        Debug.Log("Update FoldCount");
        FoldCount.text = FoldMinigameManager.ClothesComponent.clothes.ToString();
    }

    public void OnSceneChange()
    {
        //  Events.OnObjectiveUpdate.RemoveListener(UpdateCatchCount);
        Events.UpdateScore.RemoveListener(UpdateFoldCount);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
