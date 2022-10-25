using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class DisplayChildCount : MonoBehaviour
{
    public TextMeshProUGUI ChildCount;
    public HideSeekManager hideAndSeekMinigame;

    private void Awake()
    {
        SingletonManager.Register(this);

       
    }

    // Start is called before the first frame update
    void Start()
    {
        if (hideAndSeekMinigame == null)
        {
            hideAndSeekMinigame = SingletonManager.Get<HideSeekManager>();
        }

        //  Events.OnObjectiveUpdate.AddListener(UpdateCatchCount);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateChildCount()
    {
        Assert.IsNotNull(ChildCount, "Catch Count text is not set or is null");
        Assert.IsNotNull(hideAndSeekMinigame, "Fold Minigame Manager is null or is not set");
        ChildCount.text = hideAndSeekMinigame.count.ToString();
    }

    public void OnSceneChange()
    {
        //  Events.OnObjectiveUpdate.RemoveListener(UpdateCatchCount);
        Events.UpdateScore.RemoveListener(updateChildCount);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
