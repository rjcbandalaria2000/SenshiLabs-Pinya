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
        SFXManager sFXManager;
    public AudioClip foundSFX;
    int counter; 

    private void Awake()
    {
        SingletonManager.Register(this);
        sFXManager = GetComponent<SFXManager>();

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
        ChildCount.text = hideAndSeekMinigame.childCount.ToString();
        if (counter == 0)
        {
            counter++;
        }
        else
        {
            sFXManager.PlaySFX(foundSFX);
        }
       
    }

    public void OnSceneChange()
    {
        //  Events.OnObjectiveUpdate.RemoveListener(UpdateCatchCount);
        Events.UpdateScore.RemoveListener(updateChildCount);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
